using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IActionRepository))]
    public class ActionRepository : BaseRavenRepository<Action>, IActionRepository
    {
        public ActionRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        protected override Task<List<Action>> ConvertAsync(List<Action> storage)
        {
            return Task.FromResult(storage);
        }

        public async Task<List<Action>> GetAllToBeSentActionsAsync()
        {
            var result = await Session.Query<Action>().OfType<Action>().ToListAsync();
            return result;
        }

        public async Task<Interval> GetActionReminderIntervalAsync(string actionId)
        {
            var action = await Session.LoadAsync<Action>(actionId);

            Console.WriteLine("reminder Interval: " + action.ReminderInterval + "    " + DateTime.Now);

            return action.ReminderInterval;
        }

        // get actiondescription
        public async Task<string> GetActionDescriptionAsync(string actionId)
        {
            var action = await Session.LoadAsync<Action>(actionId);

            return action.Description;
        }

        public async Task<Action> SetActionReminderSettingsAsync(Interval interval, string actionId)
        {
            // load action
            var action = await Session.LoadAsync<Action>(actionId);

            // set action reminder
            action.ReminderInterval = interval;

            int todaysDayOfYear = DateTime.Now.DayOfYear;
            string todaysDayOfYearString = todaysDayOfYear.ToString();

            action.LastReminded = todaysDayOfYearString;

            // save action
            await Session.StoreAsync(action);

            return action;
        }
    }
}