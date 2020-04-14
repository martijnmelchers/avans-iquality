using System.Threading.Tasks;
using IQuality.Models;
using IQuality.Models.Authentication;

namespace IQuality.DomainServices.Interfaces
{
    public interface IInviteService
    {
        public Task<Invite> CreateInvite(string userId, string email, string groupName);
        Task ConsumeInvite(string inviteToken);
        public Task<Invite> GetInvite(string inviteToken);
        Task<bool> ValidateInvite(string inviteToken);
    }
}
