using System.Threading.Tasks;
using IQuality.Models;
using IQuality.Models.Authentication;

namespace IQuality.DomainServices.Interfaces
{
    public interface IInviteService
    {
        public Task<Invite> CreateInvite(string userId, string email);
        void ConsumeInvite(Invite invite);
        public Task<Invite> GetInvite(string Id);
        Task<bool> ValidateInvite(string inviteToken);
    }
}
