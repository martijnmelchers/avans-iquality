using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface IBuddyChatService
    {
        Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId);
    }
}
