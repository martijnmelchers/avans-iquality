using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat.Messages;
using Microsoft.AspNetCore.SignalR;

namespace IQuality.Api.Controllers
{
    public class ChatHubController : Hub
    {
        private readonly IMessageRepository _repository;

        public ChatHubController(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName)
                .SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left he group {groupName}");
        }

        public async Task<List<TextMessage>> ReceiveMessagesAsync(string chatId)
        {
            List<TextMessage> result = await _repository.GetTextMessagesByChat(chatId);
            return result;
        }

        public async Task NewMessage(string senderId, string chatId, string message)
        {
            await Clients.All.SendAsync("messageReceived", senderId, chatId, message);
            
            TextMessage textMessage = new TextMessage {SenderId = senderId, ChatId = chatId, Content = message};
            await _repository.SaveAsync(textMessage);
        }
    }
}