
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.Models.Interfaces
{
    public interface IActionService
    {
        Task<Action> CreateAction(string chatId, string goalId, string description, string actionType, string userId = null);
        Task<List<Action>> GetActions(string chatId);
        Task<bool> DeleteAction(string actionId);
    }
}