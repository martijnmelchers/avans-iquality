using System;

namespace IQuality.Models.Chat
{
    public class PatientChat : BaseChat
    {
        public PatientChat()
        {
            Intent = new IntentData();
        }
        public IntentData Intent { get; set; }
    }
}