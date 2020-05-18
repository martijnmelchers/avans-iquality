using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Actions;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IUserRepository))]
    public class UserRepository : BaseRavenRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IAsyncDocumentSession session) : base(session)
        {
        }
        
        protected override Task<List<ApplicationUser>> ConvertAsync(List<ApplicationUser> storage)
        {
            return Task.FromResult(storage.ToList());
        }
    }
}