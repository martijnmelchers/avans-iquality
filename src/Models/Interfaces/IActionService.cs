
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.Models.Interfaces
{
    public interface IActionService
    {
        Task<Action> CreateAction(string chatId, string goalId, string description, string actionType);
        Task<List<Action>> GetActions(string chatId);
        Task<Action> SetActionReminderSettingsAsync(Interval interval, string actionId);
    }
}