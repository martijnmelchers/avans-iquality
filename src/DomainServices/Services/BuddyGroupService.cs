using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IBuddyGroupService))]
    public class BuddyGroupService : IBuddyGroupService
    {
        private IBuddyGroupRepository _buddyGroupRepository;

        public BuddyGroupService(IBuddyGroupRepository buddyGroupRepository)
        {
            _buddyGroupRepository = buddyGroupRepository;
        }

        public async Task AddBuddy(Buddy buddy)
        {
            await _buddyGroupRepository.SaveAsync(buddy);
        }

        public async Task DeleteBuddy(int id)
        {
            await _buddyGroupRepository.Delete(id);
        }

        public async Task<List<Buddy>> GetBuddies()
        {
            var result = await _buddyGroupRepository.GetAll();
            return result;
        }

        public async Task<Buddy> GetBuddyById(int id)
        {
            return await _buddyGroupRepository.GetByIdAsync(id.ToString());
        }
    }
}
