using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IChatRepository : IBaseRavenRepository<BaseChat>
    {
        Task<List<BaseChat>> GetChatsAsync();
        Task<List<BaseChat>> GetChatsAsync(int skip, int take);
        Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId);
    }
}