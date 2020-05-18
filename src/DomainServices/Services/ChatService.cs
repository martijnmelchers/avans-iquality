using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Identity;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IChatService))]
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatService(IChatRepository chatRepository, UserManager<ApplicationUser> userManager)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
        }

        public async Task<ChatContext<BaseChat>> GetChatAsync(string id)
        {
            return await _chatRepository.GetByIdAsync(id);
        }

        public async Task<List<ChatContext<BaseChat>>> GetChatsAsync()
        {
            return await _chatRepository.GetChatsAsync(0, 100);
        }

        public async Task<List<ChatContext<BaseChat>>> GetChatsAsync(int skip, int take)
        {
            return await _chatRepository.GetChatsAsync(skip, take);
        }

        public async Task<ChatContext<BaseChat>> CreateChatAsync(BaseChat baseChat)
        {
            ChatContext<BaseChat> context = new ChatContext<BaseChat>
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

            if (baseChat.InitiatorId == userId) return true;

            if (baseChat.ParticipatorIds != null && baseChat.ParticipatorIds.Count != 0)
                return baseChat.ParticipatorIds.Any(participator => participator == userId);

            return false;
        }

        public async void DeleteChatAsync(string id)
        {
            _chatRepository.Delete(await _chatRepository.GetByIdAsync(id));
        }
        
        public async Task AddUserToChat(string applicationUserId, string chatId)
        {
            ChatContext<BaseChat> chat = await _chatRepository.GetByIdAsync(chatId);
            if (chat.Chat.ParticipatorIds == null)
            {
                chat.Chat.ParticipatorIds = new List<string>();
            }
            
            chat.Chat.ParticipatorIds.Add(applicationUserId);
            await _chatRepository.SaveAsync(chat);
        }

        public async Task<string> GetContactName(string userId, BaseChat chat)
        {
            if (chat.ParticipatorIds == null || chat.ParticipatorIds.Count == 0)
                throw new Exception("Chat does not have any participators");

            string contactId = userId == chat.InitiatorId ? chat.ParticipatorIds[0] : chat.InitiatorId;
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(contactId);
            
            return applicationUser.Name.ToString();
        }
    }
}