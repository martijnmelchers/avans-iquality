using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface IBuddyGroupService
    {
        Task AddBuddy(Buddy buddy);
        Task DeleteBuddy(int id);
        Task<List<Buddy>> GetBuddies();
        Task<Buddy> GetBuddyById(int id);

    }
}
