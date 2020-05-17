using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Helpers;
using System;
using System.Timers;

namespace IQuality.DomainServices
{
    [Injectable]
    public class NotificationsTimer : INotificationsTimer
    {
        private readonly IActionRepository _actionRepository;
        private Timer _timer = new Timer();
        private int counter = 0;

        public NotificationsTimer(IActionRepository actionRepository)
        {
            Console.WriteLine("Notificationstimer constructor called!!>>>>>>>>>>>>>>>>>>");
            _actionRepository = actionRepository;

            _timer.Interval = 15 * 1000;
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

        // TODO: session disposed fixen
        private async void Start()
        {
            TimeSpan start = new TimeSpan(8, 0, 0); //9 o'clock
            TimeSpan end = new TimeSpan(23, 0, 0); //10 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                // send notification

                // get all actions
                try
                {
                    var result = await _actionRepository.GetAllToBeSentActionsAsync("actions-1-A");
                    //Console.WriteLine(result.Count);
                }catch(ObjectDisposedException e)
                {
                    //Console.WriteLine(e.Message);
                }
            }

            _timer.Start();
        }
    }
}

