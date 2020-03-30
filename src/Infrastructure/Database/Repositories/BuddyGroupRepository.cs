using IQuality.DomainServices.Interfaces.Repositories;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Repositories
{
    public class BuddyGroupRepository : BaseRavenRepository<Buddy>
    {
        private readonly IAsyncDocumentSession _session;
 
      

        public BuddyGroupRepository(IAsyncDocumentSession session):base(session)
        {
            _session = session;
        }

        public async Task<string> AddBuddy(Buddy buddy)
        {
            await _session.StoreAsync(buddy);
            return buddy.Id;
        }

        public override Task DeleteAsync(Buddy entity)
        {
            
            throw new NotImplementedException();
        }

        public async Task<int> DeleteBuddy(int id)
        {
            //var result = await iets;
            throw new NotImplementedException();
        }

        public async Task<List<Buddy>> GetAll()
        {
            //var result = await iets;
            throw new NotImplementedException();
        }
        public async Task<int> UpdateBuddy(int id, Buddy buddy)
        {
            //var result = await iets;
            throw new NotImplementedException();
        }

        public async Task<Buddy> GetByID(int id)
        {
            //var result = await iets;
            throw new NotImplementedException();
        }

        public override Task SaveAsync(Buddy entity)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<Buddy>> ConvertAsync(IEnumerable<Buddy> storage)
        {
            throw new NotImplementedException();
        }
    }
}

