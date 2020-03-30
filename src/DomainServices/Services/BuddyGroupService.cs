using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;

namespace IQuality.DomainServices.Services
{
    public class BuddyGroupService : IBuddyGroupService
    {
        private IBuddyGroupRepository _buddyGroupRepository;

        public BuddyGroupService(IBuddyGroupRepository buddyGroupRepository)
        {
            _buddyGroupRepository = buddyGroupRepository;
        }

        public Task<int> AddBuddy(Buddy buddy)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBuddy(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Buddy>> GetBuddies()
        {
            var result = await _buddyGroupRepository.GetAll();
            return result;
        }

        public Task<Buddy> GetBuddyById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateBuddy(int id, Buddy buddy)
        {
            throw new NotImplementedException();
        }
    }
}
