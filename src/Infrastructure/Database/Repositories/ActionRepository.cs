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

        public async Task<List<Action>> GetAllToBeSentActionsAsync(string actionId)
        {
            try
            {
                //int yesterdaysDayOfYear = DateTime.Now.DayOfYear - 1;
                int yesterdaysDayOfYear = DateTime.Now.DayOfYear;
                string yesterdaysDayOfYearString = yesterdaysDayOfYear.ToString();

                int lastWeeksDayOfYear = DateTime.Now.DayOfYear - 7;
                string lastWeeksDayOfYearString = yesterdaysDayOfYear.ToString();

                int lastMonthsDayOfYear = DateTime.Now.DayOfYear - 31;
                string lastMonthsDayOfYearString = yesterdaysDayOfYear.ToString();

                var result = await Session.Query<Action>().OfType<Action>().ToListAsync();
                //var result = await Session.LoadAsync<Action>(actionId);
                //var result = await Session.Query<Action>().Where(a => a.LastReminded == yesterdaysDayOfYearString).ToListAsync();
                return null;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
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

        public async Task<List<string>> GetActionTypesOfGoalIds(List<string> goalIds)
        {
            List<Action> actionsOfGoal = new List<Action>();

            foreach (string goalId in goalIds)
            {
                actionsOfGoal.AddRange(await Session.Query<Action>().OfType<Action>().Where(a => a.GoalId == goalId).ToListAsync());
            }

            List<string> usedActionTypes = new List<string>();

            foreach (Action action in actionsOfGoal)
            {
                if (!usedActionTypes.Contains(action.Type.ToString()))
                {
                    usedActionTypes.Add(action.Type.ToString());
                }
            }

            return usedActionTypes;
        }
    }
}