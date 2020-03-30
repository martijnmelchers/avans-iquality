using IQuality.DomainServices.Interfaces.Repositories;
using IQuality.Models;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Repositories
{
    public class BuddyGroupRepository : IBuddyGroupRepository
    {
        private readonly IAsyncDocumentSession _session;

        public BuddyGroupRepository()
        {

        }

        public BuddyGroupRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task<string> AddBuddy(Buddy buddy)
        {
            Buddy buddyToBeSaved = new Buddy
            {
                GroupId = buddy.GroupId,
                Name = buddy.Name,
                PhoneNumber = buddy.PhoneNumber,
                ImagePath = buddy.ImagePath
            };

            await _session.StoreAsync(buddyToBeSaved);
            return buddyToBeSaved.Id;
            //var result = await iets; // session call
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

        public async Task<Buddy> GetByID(int id)
        {
            //var result = await iets;
            throw new NotImplementedException();
        }

        public async Task<int> UpdateBuddy(int id, Buddy buddy)
        {
            //var result = await iets;
            throw new NotImplementedException();
        }
    }
}

