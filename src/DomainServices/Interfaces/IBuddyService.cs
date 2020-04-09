using IQuality.Models;
using IQuality.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface IBuddyService
    {
        Task AddBuddy(Buddy buddy);
        Task UpdateBuddy(Buddy buddy);
        Task DeleteBuddy(string id);
        Task<List<Buddy>> GetBuddies();
        Task<Buddy> GetBuddyById(string id);
    }
}
