using IQuality.Models;
using IQuality.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface IBuddyGroupService
    {
        Task<List<string>> GetBuddygroupNames();
        Task<List<Buddy>> GetBuddiesByGroupName(string groupName);
    }
}
