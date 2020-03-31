using System.Threading.Tasks;
using IQuality.Models;
using IQuality.Models.Authentication;

namespace IQuality.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(bool success, ApplicationUser user)> Login(string email, string password);
        Task<ApplicationUser> Register(ApplicationUser user, string password);
        string GenerateToken(ApplicationUser user);
        Task CreateInvite(string userId);
        void RespondInvite(Invite link, bool accepted = true);
        public Task<Invite> GetInvite(string Id);
    }
}
