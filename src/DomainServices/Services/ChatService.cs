using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Helpers;
using Sparrow.Json;


namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IChatService))]
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;

        public ChatService(IChatRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<BaseChat> GetChatAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<BaseChat>> GetChatsAsync()
        {
            return await _repository.GetChatsAsync();
        }
        
        public async Task<BaseChat> CreateChatAsync(BaseChat baseChat)
        {
            await _repository.SaveAsync(baseChat);
            return baseChat;
        }

        public async void DeleteChatAsync(string id)
        {
            BaseChat chat = await _repository.GetByIdAsync(id);
            _repository.DeleteAsync(chat);
        }
    }
}