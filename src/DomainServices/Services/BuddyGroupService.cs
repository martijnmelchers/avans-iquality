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

        public async Task<List<string>> GetBuddygroupNames()
        {
            var result = await _buddyGroupRepository.GetAll();
            return result;
        }

        public async Task<List<Buddy>> GetBuddiesByGroupName(string groupName)
        {
            var result = await _buddyGroupRepository.GetBuddiesByGroupName(string groupName);
            return result;
        }
    }
}
