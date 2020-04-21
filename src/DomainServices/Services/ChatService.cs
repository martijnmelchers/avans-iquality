using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Helpers;


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
            return await _chatRepository.GetChatsAsync<BaseChat>();
        }

        public async Task<List<BaseChat>> GetChatsAsync(int skip, int take)
        {
            return await _chatRepository.GetChatsAsync<BaseChat>(skip, take);
        }

        public async Task<BaseChat> CreateChatAsync(BaseChat baseChat)
        {
            await _chatRepository.SaveAsync(baseChat);
            return baseChat;
        }

        public async Task<bool> UserCanJoinChat(string userId, string chatId)
        {
            BaseChat baseChat;
            try
            {
                 baseChat = await GetChatAsync(chatId);
            }
            catch (Exception e)
            {
                return false;
            }
            
            if (baseChat.InitiatorId == userId)
            {
                return true;
            }

            if (baseChat.ParticipatorIds != null && baseChat.ParticipatorIds.Count != 0)
            {
                foreach (string participator in baseChat.ParticipatorIds)
                {
                    if (participator == userId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async void DeleteChatAsync(string id)
        {
            BaseChat chat = await _chatRepository.GetByIdAsync(id);
            _chatRepository.Delete(chat);
        }
    }
}
