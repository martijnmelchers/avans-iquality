using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;
using IQuality.Models;
using Raven.Client.Documents;
using System;
using System.Text;

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

        public async Task Delete(string id)
        {
            Buddy buddy = await _session.LoadAsync<Buddy>(id);
            _session.Delete(buddy);
        }

        protected override async Task<List<Buddy>> ConvertAsync(List<Buddy> storage)
        {
            return await Task.FromResult(storage);
        }

        public async Task<Buddy> GetBuddyById(string id)
        {
            return await _session.LoadAsync<Buddy>(id);
        }

        public override async Task SaveAsync(Buddy entity)
        {
            await _session.StoreAsync(entity);
            return;
        }

        public override void Delete(Buddy entity)
        {
            throw new NotImplementedException();
        }
    }
}
