using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Actions;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;

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
        
        public async Task<Action> CreateAction(string chatId, string goalId, string description)
        {
            var action = new Action
            {
                // TODO: Make dynamic
                Type = ActionType.Weight,
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
        
    }
}