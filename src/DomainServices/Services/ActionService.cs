using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Actions;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;
using IQuality.Models.Measurements;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;

        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }
        
        public async Task<Action> CreateAction(string chatId, string goalId, string description, string actionType)
        {
            Enum.TryParse(actionType, out ActionType type);
            var action = new Action()
            {
                Type =  type,
                Description = description,
                GoalId = goalId,
                ChatId = chatId
            };

            await _actionRepository.SaveAsync(action);
            return action;
        }

        public async Task AddMeasurement(string goalId, double value)
        {
            var action = await _actionRepository.GetByIdAsync(goalId);
            
            action.Measurements.Add(new Measurement(value));
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