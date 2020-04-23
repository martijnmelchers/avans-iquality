using System;

namespace IQuality.Models.Chat
{
    public class PatientChat : BaseChat
    {
        public string IntentName { get; set; }
        public DateTime IntentStartDate { get; set; }
    }
}