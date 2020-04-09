using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using Sparrow.Json;


namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IChatService))]
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
        
        public async Task<BaseChat> GetChatAsync(string id)
        {
            return await _chatRepository.GetByIdAsync(id);
        }

        public async Task<List<BaseChat>> GetChatsAsync()
        {
            return await _chatRepository.GetChatsAsync();
        }
        
        public async Task<BaseChat> CreateChatAsync(BaseChat baseChat)
        {
            await _chatRepository.SaveAsync(baseChat);
            return baseChat;
        }

        public async void DeleteChatAsync(string id)
        {
            BaseChat chat = await _chatRepository.GetByIdAsync(id);
            _chatRepository.DeleteAsync(chat);
        }
    }
}