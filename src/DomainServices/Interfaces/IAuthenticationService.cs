using System.Threading.Tasks;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Forms;

namespace IQuality.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(bool success, ApplicationUser user)> Login(string email, string password);
        Task<(string chatId, ApplicationUser user)> Register(string inviteToken, UserRegister register);
        string GenerateToken(ApplicationUser user);
    }
}
