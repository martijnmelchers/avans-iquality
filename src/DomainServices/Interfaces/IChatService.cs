using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;

namespace IQuality.DomainServices.Interfaces
{
    public interface IChatService 
    {
        public Task<BaseChat> GetChatAsync(string id);
        public Task<List<BaseChat>> GetChatsAsync();
        public Task<List<BaseChat>> GetChatsAsync(int skip, int take);

        public Task<BaseChat> CreateChatAsync(BaseChat chat);
        public Task<Boolean> UserCanJoinChat(string userId, string chatId);
        public void DeleteChatAsync(string id);
        public Task<PatientChat> GetPatientChatIncludeGoalsAsync(string chatId);
    }
}