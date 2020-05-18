using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        //private bool _notificationTimerSet = false;

        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }
        
        public async Task<Action> CreateAction(string chatId, string goalId, string description, string actionType)
        {
            ActionType type;
            ActionType.TryParse(actionType, out type);
            Console.WriteLine(chatId, goalId, description, actionType);
            var action = new Action
            {
                Type =  type,
                Description = description,
                GoalId = goalId,
                ChatId = chatId
            };

            await _actionRepository.SaveAsync(action);
            return action;
        }

        public async Task<List<Action>> GetActions(string chatId)
        {
           List<Action> results  = await _actionRepository.GetAllWhereAsync(p => p.ChatId == chatId);
           return results;
        }

        public Task<bool> DeleteAction(string actionId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Action> SetActionReminderSettingsAsync(Interval interval, string actionId)
        {
            var result = await _actionRepository.SetActionReminderSettingsAsync(interval, actionId);

            return result;
        }
    }
}