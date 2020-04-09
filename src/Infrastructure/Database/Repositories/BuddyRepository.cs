using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IBuddyRepository))]
    public class BuddyRepository : BaseRavenRepository<Buddy>, IBuddyRepository
    {
        public BuddyRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        public override async Task SaveAsync(Buddy entity)
        {
            await Session.StoreAsync(entity);
        }

        public override void Delete(Buddy entity)
        {
            Session.Delete(entity);
        }

        protected override async Task<List<Buddy>> ConvertAsync(List<Buddy> storage)
        {
            return await Task.FromResult(storage);
        }
    }
}