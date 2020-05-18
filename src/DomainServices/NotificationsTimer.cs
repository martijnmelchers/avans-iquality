using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Linq;
using System.Timers;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.DomainServices
{
    public class NotificationsTimer : INotificationsTimer
    {
        private Timer _timer = new Timer();
        private int counter = 0;
        private readonly IDocumentStore _documentStore;

        public NotificationsTimer(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void StartTimer(int interval)
        {
            _timer.Interval = interval * 1000;
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = false;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (counter == 0)
            {
                Start();
            }
        }

        private async void Start()
        {
            // TODO: set TimeSpan start to 9 and TimeSpan end to 10
            TimeSpan start = new TimeSpan(8, 0, 0); //9 o'clock
            TimeSpan end = new TimeSpan(23, 0, 0); //10 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                // send notification
                try
                {
                    using (IAsyncDocumentSession Session = _documentStore.OpenAsyncSession())
                    {
                        var actionRepository = new ActionRepository(Session);
                        var chatRepository = new ChatRepository(Session);
                        var patientRepository = new PatientRepository(Session);

                        var reminderActions = await actionRepository.GetAllReminderActionsAsync();

                        // TODO: remove line 60, uncomment line 61
                        int yesterdaysDayOfYear = DateTime.Now.DayOfYear;
                        //int yesterdaysDayOfYear = DateTime.Now.DayOfYear - 1;
                        string yesterdaysDayOfYearString = yesterdaysDayOfYear.ToString();

                        int lastWeeksDayOfYear = DateTime.Now.DayOfYear - 7;
                        string lastWeeksDayOfYearString = lastWeeksDayOfYear.ToString();

                        int lastMonthsDayOfYear = DateTime.Now.DayOfYear - 31;
                        string lastMonthsDayOfYearString = lastMonthsDayOfYear.ToString();

                        foreach (Action action in reminderActions)
                        {
                            if (action.LastReminded == yesterdaysDayOfYearString || action.LastReminded == lastWeeksDayOfYearString || action.LastReminded == lastMonthsDayOfYearString)
                            {
                                // it's time to remind all people with notification devices now.
                                var patientChat = await chatRepository.GetPatientChatAsync(action.ChatId);

                                var notificationIds = await patientRepository.GetNotificationIdsOfPatient(patientChat.ParticipatorIds.First());

                                if (notificationIds != null)
                                {
                                    foreach (string id in notificationIds)
                                    {
                                        // send onesignal messages

                                    }
                                }
                            }
                        }

                        // check weekly monthly
                        // check of de patient waar deze action van is een notificationId heeft
                        // zo ja, stuur een web push naar dat device

                        // in de frontend:
                        // vragen om notification permission            (check hoe je al die onesignal dingen met Angular kan doen)
                        // als patient permission geeft, sla token in db op

                        await Session.SaveChangesAsync();
                    }
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            _timer.Start();
        }
    }
}

