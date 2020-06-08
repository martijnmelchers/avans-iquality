using System.Threading.Tasks;
using IQuality.Models;
using IQuality.Models.Authentication;

namespace IQuality.DomainServices.Interfaces
{
    public interface IInviteService
    {
        public Task<Invite> CreateInvite(string userId, string email, string chatId, string chatName);
        Task<string> ConsumeInvite(string inviteToken, string applicationUserId);
        public Task<Invite> GetInvite(string inviteToken);
        Task<bool> ValidateInvite(string inviteToken);
        Task SendInviteEmail(Invite inv);
    }
}
