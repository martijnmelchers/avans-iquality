using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ChatContext<BaseChat>> GetChatAsync(string id)
        {
            return await _chatRepository.GetByIdAsync(id);
        }

        public async Task<List<ChatContext<BaseChat>>> GetChatsAsync()
        {
            return await _chatRepository.GetChatsAsync(0, 10);
        }

        public async Task<List<ChatContext<BaseChat>>> GetChatsAsync(int skip, int take)
        {
            return await _chatRepository.GetChatsAsync(skip, take);
        }
        
        public async Task<ChatContext<BaseChat>> CreateChatAsync(BaseChat baseChat)
        {
            var context = new ChatContext<BaseChat>()
            {
                Chat = baseChat
            };
            await _chatRepository.SaveAsync(context);
            return context;
        }

        public async Task<bool> UserCanJoinChat(string userId, string chatId)
        {
            BaseChat baseChat;
            try
            {
                 baseChat = (await GetChatAsync(chatId)).Chat;
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
                return baseChat.ParticipatorIds.Any(participator => participator == userId);
            }

            return false;
        }

        public async void DeleteChatAsync(string id)
        {
            _chatRepository.Delete(await _chatRepository.GetByIdAsync(id));
        }
    }
}
