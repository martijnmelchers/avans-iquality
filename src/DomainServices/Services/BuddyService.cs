using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IBuddyService))]
    public class BuddyService : IBuddyService
    {
        private IBuddyRepository _buddyRepository;

        public BuddyService(IBuddyRepository buddyRepository)
        {
            _buddyRepository = buddyRepository;
        }

        public async Task AddBuddy(Buddy buddy)
        {
            await _buddyRepository.SaveAsync(buddy);
        }

        public async Task DeleteBuddy(string id)
        {
            await _buddyRepository.Delete(id);
        }

        public async Task<List<Buddy>> GetBuddies()
        {
            var result = await _buddyRepository.GetAll();
            return result;
        }

        public async Task<Buddy> GetBuddyById(int id)
        {
            return await _buddyRepository.GetByIdAsync(id.ToString());
        }
    }
}
