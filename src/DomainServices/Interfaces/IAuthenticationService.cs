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
        public void CreateInvite(RegistrationLink link);
        void RespondInvite(RegistrationLink link, bool accepted = true);
        public Task<RegistrationLink> GetInvite(string Id);
    }
}
