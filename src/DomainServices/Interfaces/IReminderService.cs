using IQuality.Models.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface IReminderService
    {
        Task<List<Reminder>> GetAllRemindersOfTodayAsync(string userId);
        Task<Reminder> CreateReminderAsync(Reminder reminder);
        Task<List<Reminder>> GetRemindersOfTodayAsync(string userId);
    }
}