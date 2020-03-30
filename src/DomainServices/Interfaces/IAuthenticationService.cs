using System.Threading.Tasks;
using IQuality.Models;

namespace IQuality.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(bool success, ApplicationUser user)> Login(string email, string password);
        Task<ApplicationUser> Register(ApplicationUser user, string password);
        string GenerateToken(ApplicationUser user);
    }
}