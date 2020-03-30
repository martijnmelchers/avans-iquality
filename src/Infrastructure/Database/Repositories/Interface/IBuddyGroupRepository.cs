using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IBuddyGroupRepository
    {
        Task<List<Buddy>> GetAll();
        Task<Buddy> GetByID(int id);
        Task<Buddy> AddBuddy(Buddy buddy);
        Task<int> UpdateBuddy(int id, Buddy buddy);
        Task<int> DeleteBuddy(int id);
    }
}
