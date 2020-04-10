using IQuality.Infrastructure.Database.Repositories;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
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

        public async Task<List<string>> GetAll()
        {
            var buddies = await _session.Query<Buddy>().ToListAsync();
            List<string> buddygroupNames = new List<string>();

            foreach(Buddy buddy in buddies)
            {
                if (!buddygroupNames.Contains(buddy.GroupName))
                {
                    buddygroupNames.Add(buddy.GroupName);
                }
            }

            return buddygroupNames;
        }

         public async Task<List<Buddy>> GetBuddiesByGroupName(string groupName)
        {
            var buddies = await _session.Query<Buddy>().ToListAsync();
            List<Buddy> groupBuddies = new List<Buddy>();

            foreach (Buddy buddy in buddies)
            {
                if (buddy.GroupName.ToLower() == groupName.ToLower())
                {
                    groupBuddies.Add(buddy);
                }
            }

            return groupBuddies;
        }

        public async Task Delete(int id)
        {
            Buddy buddy = await _session.LoadAsync<Buddy>(id.ToString());
            _session.Delete(buddy);
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

        protected override Task<List<Buddy>> ConvertAsync(List<Buddy> storage)
        {
            throw new NotImplementedException();
        }
    }
}

