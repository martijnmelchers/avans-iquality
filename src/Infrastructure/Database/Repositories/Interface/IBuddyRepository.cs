using IQuality.Models.Authentication;
using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IBuddyRepository : IBaseRavenRepository<Buddy>
    {
        Task<List<Buddy>> GetAll();
        Task Delete(string id);
        Task<Buddy> GetBuddyById(string id);
    }
}