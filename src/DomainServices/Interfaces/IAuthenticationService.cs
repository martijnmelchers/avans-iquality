using System.Threading.Tasks;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Forms;
using Nancy.Bootstrapper;

namespace IQuality.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(bool success, ApplicationUser user)> Login(string email, string password);
        Task<(string chatId, ApplicationUser user)> Register(string inviteToken, UserRegister register);
        Task<bool> ApplicationUserExists(string email);
        string GenerateToken(ApplicationUser user);
    }
}
