using IQuality.Models.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IReminderRepository : IBaseRavenRepository<Reminder>
    {
        Task<List<Reminder>> GetAllRemindersOfTodayAsync(string userId);
        Task<List<Reminder>> GetRemindersOfTodayAsync(string userId);
        Task GenenerateReminders(string userId, string actionId, string description);
    }
}
