using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
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

        public async Task<List<Action>> GetAllReminderActionsAsync()
        {
            return await Session.Stream<Action>(Session.Query<Action>().Where(a => a.ReminderInterval != Interval.Never));
        }

        public async Task<Interval> GetActionReminderIntervalAsync(string actionId)
        {
            var action = await Session.LoadAsync<Action>(actionId);

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

            // set last reminded day
            action.LastReminded = todaysDayOfYearString;

            // save action
            await Session.StoreAsync(action);

            return action;
        }

        public async Task<Action> SetLastRemindedToTodayAsync(string actionId)
        {
            if (actionId != null && actionId != "")
            {
                Action action;
                try
                {
                    action = await Session.LoadAsync<Action>(actionId);
                } catch (Exception e)
                {
                    return new Action();
                }

                int todaysDayOfYear = DateTime.Now.DayOfYear;
                string todaysDayOfYearString = todaysDayOfYear.ToString();

                action.LastReminded = todaysDayOfYearString;

                await Session.StoreAsync(action);

                return action;
            }

            return new Action();
        }

        public async Task<List<Action>> GetActionsOfGoalIds(List<string> goalIds)
        {
            if (goalIds != null && goalIds.Count != 0)
            {
                List<Action> actionsOfGoal = new List<Action>();

                foreach (string goalId in goalIds)
                {
                    actionsOfGoal.AddRange(await Session.Query<Action>().Where(a => a.GoalId == goalId).ToListAsync());
                }

                return actionsOfGoal;
            }

            return new List<Action>();
        }

        public async Task<List<string>> GetActionTypesOfGoalIds(List<string> goalIds)
        {
            List<Action> actionsOfGoal = new List<Action>();

            foreach (string goalId in goalIds)
            {
                actionsOfGoal.AddRange(await Session.Query<Action>().Where(a => a.GoalId == goalId).ToListAsync());
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