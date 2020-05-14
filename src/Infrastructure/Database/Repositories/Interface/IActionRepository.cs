using IQuality.Models.Actions;
using IQuality.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IActionRepository : IBaseRavenRepository<Action>
    {
        Task<Action> SetActionReminderSettingsAsync(Interval interval, string actionId);
        Task<Interval> GetActionReminderIntervalAsync(string actionId);
        Task<string> GetActionDescriptionAsync(string actionId);
        Task<List<Action>> GetAllToBeSentActionsAsync();
    }
}