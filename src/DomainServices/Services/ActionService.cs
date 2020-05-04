
using System;
﻿using System.Collections.Generic;
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
        

        public async Task<Action> CreateAction(string chatId, string goalId, string description, string actionType, string userId = null)
        {
            ActionType type;
            ActionType.TryParse(actionType, out type);
            var action = new Action
            {
                Type =  type,
                Description = description,
                ReminderInterval = Interval.Daily,
                GoalId = goalId,
                ChatId = chatId
            };

            await _reminderRepository.GenenerateReminders(userId, action.Id, action.Description);

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
        
    }
}