using System.Threading.Tasks;
using IQuality.Models;

namespace IQuality.DomainServices.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(bool success, ApplicationUser user)> Login(string email, string password);
        string GenerateToken(ApplicationUser user);
    }
}