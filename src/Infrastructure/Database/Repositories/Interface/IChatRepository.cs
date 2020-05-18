using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IChatRepository : IBaseRavenRepository<ChatContext<BaseChat>, BaseChat>
    {
        Task<List<ChatContext<BaseChat>>> GetChatsAsync(string userId, int skip, int take);
        Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId);
        Task<PatientChat> GetPatientChatAsync(string chatId);
        Task<string> GetPatientChatByPatientId(string patientId);
    }
}