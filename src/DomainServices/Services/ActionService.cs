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
        
        public async Task CreateAction(string chatId, string description)
        {
            var action = new Action
            {
                // TODO: Make dynamic
                Type = ActionType.Weight,
                Description = description,
                ChatId = chatId
            };

            await _actionRepository.SaveAsync(action);
        }
    }
}