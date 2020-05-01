using IQuality.Models.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IReminderRepository : IBaseRavenRepository<Reminder>
    {
        Task<List<Reminder>> GetRemindersByUserIdAsync(string userId);
    }
}
