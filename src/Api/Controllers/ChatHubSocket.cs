using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    
    public class ChatHubSocket : Hub
    {
        private readonly MessageRepository _repository;

        public ChatHubSocket(MessageRepository repository)
        {
            _repository = repository;
        }
        
        public async Task SendMessage(string senderId, string chatId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderId, chatId, message);
            
            TextMessage textMessage = new TextMessage {SenderId = senderId, ChatId = chatId, Content = message};
            await _repository.SaveAsync(textMessage);

        }
    }
}