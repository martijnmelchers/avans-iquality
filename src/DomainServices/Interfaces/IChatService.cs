using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;

namespace IQuality.DomainServices.Interfaces
{
    public interface IChatService
    {
        public Task<BaseChat> GetChatAsync(string id);
        public Task<List<BaseChat>> GetChatsAsync();
        
        public Task<BaseChat> CreateChatAsync(BaseChat baseChat);
        public void DeleteChatAsync(string id);
    }
}