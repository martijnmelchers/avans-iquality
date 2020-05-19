using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories;
using Nancy.Json;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
                                        var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                                        request.KeepAlive = true;
                                        request.Method = "POST";
                                        request.ContentType = "application/json; charset=utf-8";
                                        var serializer = new JavaScriptSerializer();
                                        var obj = new
                                        {
                                            app_id = "83238485-a09c-4593-ae5b-0281f6495b79",
                                            contents = new { en = action.Description },
                                            include_player_ids = new string[] { id }
                                        };



                                        var param = serializer.Serialize(obj);
                                        byte[] byteArray = Encoding.UTF8.GetBytes(param);

                                        string responseContent = null;
                                        try
                                        {
                                            using (var writer = request.GetRequestStream())
                                            {
                                                writer.Write(byteArray, 0, byteArray.Length);
                                            }

                                            using (var response = request.GetResponse() as HttpWebResponse)
                                            {
                                                using (var reader = new StreamReader(response.GetResponseStream()))
                                                {
                                                    responseContent = reader.ReadToEnd();
                                                }
                                            }
                                        }
                                        catch (WebException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            Console.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                                        }

                                        Console.WriteLine(responseContent);

                                    }
                                }
                            }
                        }

                        // check weekly monthly
                        // check of de patient waar deze action van is een notificationId heeft
                        // zo ja, stuur een web push naar dat device


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

