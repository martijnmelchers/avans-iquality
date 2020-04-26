using System;
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

        public ChatHub(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
        }

        private string GetSenderId()
        {
            return ((ClaimsIdentity) Context.User.Identity).Claims.First().Value;
        }

        private string GetUserName()
        {
            return ((ClaimsIdentity) Context.User.Identity).Claims.ToArray()[1].Value;
        }

        // TODO: Make a function which accepts a array of room I'd.
        public async Task JoinGroup(string groupName)
        {
            string userName = GetSenderId();

            if (!await _chatService.UserCanJoinChat(userName, groupName))
            {
                await Clients.Caller.SendAsync("SendError", "Something went wrong while joining the group");
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
            string senderName = GetUserName();
            await Clients.Group(chatId).SendAsync("messageReceived", senderId, senderName, chatId, message);

            TextMessage textMessage = new TextMessage
            {
                SenderId = senderId,
                SenderName = senderName,
                ChatId = chatId,
                Content = message,
                SendDate = DateTime.Now.ToString("H:mm")
            };
            await _messageService.PostMessage(textMessage);
        }

        public async Task DeleteMessage(string groupName, string messageId)
        {
            if (!await _messageService.RemoveMessage(groupName, messageId))
            {
                await Clients.Caller.SendAsync("SendError", "Something went wrong while removing the message");
                return;
            }

            await Clients.Group(groupName).SendAsync("removeMessage", messageId, groupName);
        }
    }
}