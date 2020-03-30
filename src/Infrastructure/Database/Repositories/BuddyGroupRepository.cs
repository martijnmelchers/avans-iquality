using IQuality.Infrastructure.Database.Repositories;
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
    [Injectable(interfaceType: typeof(IBuddyGroupRepository))]
    public class BuddyGroupRepository : BaseRavenRepository<Buddy>, IBuddyGroupRepository
    {
        private readonly IAsyncDocumentSession _session;

        public BuddyGroupRepository(IAsyncDocumentSession session):base(session)
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

        public async Task Delete(int id)
        {
            Buddy buddy = await _session.LoadAsync<Buddy>(id.ToString());
            _session.Delete(buddy);
        }

        public override Task SaveAsync(Buddy entity)
        {
            _session.StoreAsync(entity);
            return Task.CompletedTask;
        }

        protected override Task<List<Buddy>> ConvertAsync(IEnumerable<Buddy> storage)
        {
            throw new NotImplementedException();
        }
    }
}

