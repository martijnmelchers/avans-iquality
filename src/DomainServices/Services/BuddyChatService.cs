using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Helpers;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;
using IQuality.Infrastructure.Database.Repositories;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IBuddyChatService))]
    public class BuddyChatService : IBuddyChatService
    {
        private IChatRepository _chatRepository;

        public BuddyChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId)
        {
            return await _chatRepository.GetBuddyChatsByUserId(userId);
        }
    }
}
