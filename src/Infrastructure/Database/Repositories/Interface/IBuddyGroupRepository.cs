using IQuality.Models;
using IQuality.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IBuddyGroupRepository : IBaseRavenRepository<Buddy>
    {
        Task<List<string>> GetAll();
        Task<List<Buddy>> GetBuddiesByGroupName(string groupName);
        Task Delete(int id);
    }
}
