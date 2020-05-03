using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IReminderService))]
    public class ReminderService : IReminderService
    {
        private IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task<List<Reminder>> GetAllRemindersOfTodayAsync(string userId)
        {
            return await _reminderRepository.GetAllRemindersOfTodayAsync(userId);
        }

        public async Task<Reminder> CreateReminderAsync(Reminder reminder)
        {
            await _reminderRepository.SaveAsync(reminder);

            return reminder;
        }

        public async Task<List<Reminder>> GetRemindersOfTodayAsync(string userId)
        {
            return await _reminderRepository.GetRemindersOfTodayAsync(userId);
        }
    }
}
