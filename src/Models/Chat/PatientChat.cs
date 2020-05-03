using System;
using System.Collections.Generic;
using IQuality.Models.Goals;

namespace IQuality.Models.Chat
{
    public class PatientChat : BaseChat
    {
        public PatientChat()
        {
        }
        
        public string IntentName { get; set; }
        public DateTime IntentStartDate { get; set; }
        public string IntentType { get; set; }
        public string UpdateSelectedGoal { get; set; }
        
        public void ClearIntent()
        {
            IntentName = string.Empty;
            IntentType = string.Empty;
            
            UpdateSelectedGoal = "";
            IntentStartDate = DateTime.MinValue;
        }
    }
}