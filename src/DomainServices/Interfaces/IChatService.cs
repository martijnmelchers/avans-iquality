using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;

namespace IQuality.DomainServices.Interfaces
{
    public interface IChatService 
    {
        Task<ChatContext<BaseChat>> GetChatAsync(string id);
        Task<List<ChatContext<BaseChat>>> GetChatsAsync(string userId);
        Task<List<ChatContext<BaseChat>>> GetChatsAsync(string userId, int skip, int take);
        Task<ChatContext<BaseChat>> CreateChatAsync(BaseChat chat);
        Task<Boolean> UserCanJoinChat(string userId, string chatId);
        void DeleteChatAsync(string id);
        
        Task<string> GetContactName(string userId, BaseChat chat);
    }
}