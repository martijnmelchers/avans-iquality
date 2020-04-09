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

        
    }
}
