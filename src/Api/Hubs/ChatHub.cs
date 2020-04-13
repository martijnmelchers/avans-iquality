using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Chat.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IQuality.Api.Hubs
{
    [Authorize]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    // ReSharper disable ClassNeverInstantiated.Global
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly string _messageNotAllowedToJoin = "You are not allowed to join";

        private readonly IMessageService _messageService;

        private string GetSenderId()
        {
            return ((ClaimsIdentity)Context.User.Identity).Claims.First().Value;
        }
        
        public ChatHub(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
        }

        public async Task JoinGroup(string groupName)
        {
            string userName = GetSenderId();
            
            if (!await _chatService.UserCanJoinChat(userName, groupName))
            {
                await Clients.Caller.SendAsync("SendError", "Dafuq gebeurt er");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName)
                .SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left he group {groupName}");
        }

        public async Task NewMessage(string chatId, string message)
        {
            string senderId = GetSenderId();
            await Clients.Group(chatId).SendAsync("messageReceived", senderId, message);

            TextMessage textMessage = new TextMessage {SenderId = senderId, ChatId = chatId, Content = message};
            await _messageService.PostMessage(textMessage);
        }
    }
}