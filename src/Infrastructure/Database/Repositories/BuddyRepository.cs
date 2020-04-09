using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IBuddyRepository))]
    public class BuddyRepository : BaseRavenRepository<Buddy>, IBuddyRepository
    {
        private readonly IAsyncDocumentSession _session;

        public BuddyRepository(IAsyncDocumentSession session) : base(session)
        {
            _session = session;
        }

        public async Task<List<Buddy>> GetAll()
        {
            return await _session.Query<Buddy>().ToListAsync();
        }

        public override Task DeleteAsync(Buddy entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            Buddy buddy = await _session.LoadAsync<Buddy>(id);
            _session.Delete(buddy);
        }

        public override async Task SaveAsync(Buddy entity)
        {
            await _session.StoreAsync(entity);
            return;
        }

        protected override Task<List<Buddy>> ConvertAsync(IEnumerable<Buddy> storage)
        {
            throw new NotImplementedException();
        }

        public async Task<Buddy> GetBuddyById(string id)
        {
            return await _session.LoadAsync<Buddy>(id);
        }
    }
}
