using System;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IReminderRepository _reminderRepository;

        public ActionService(IActionRepository actionRepository, IReminderRepository reminderRepository)
        {
            _actionRepository = actionRepository;
            _reminderRepository = reminderRepository;
        }
        
        public async Task CreateAction(string chatId, string description, string userId)
        {
            var action = new Action
            {
                // TODO: Make dynamic
                Type = ActionType.Weight,
                Description = description,
                ChatId = chatId,
                ReminderInterval = Interval.Daily
            };

            await _reminderRepository.GenenerateReminders(userId, action.Id, action.Description);

            await _actionRepository.SaveAsync(action);
        }
    }
}