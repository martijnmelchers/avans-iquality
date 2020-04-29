using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IChatRepository : IBaseRavenRepository<BaseChat>
    {
        Task<List<T>> GetChatsAsync<T>() where T : BaseChat;
        Task<T> GetChatAsync<T>(string roomId) where T : BaseChat;
        Task<List<T>> GetChatsAsync<T>(int skip, int take) where T : BaseChat;
        Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId);
        Task<PatientChat> GetPatientChatIncludeGoalsAsync(string roomId);
    }
}