using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Authentication;
using IQuality.Models.Chat.Messages;

namespace IQuality.DomainServices.Interfaces
{
    public interface IUserService
    { 
        Task<List<ApplicationUser>> GetUsers();
        Task DeactivateUser(string applicationUserId);
        Task DeleteUser(string applicationUserId);
        Task<ApplicationUser> GetUser(string applicationUserId);
    }
}