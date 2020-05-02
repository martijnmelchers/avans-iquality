﻿using System;
using System.Collections.Generic;
using IQuality.Models.Goals;

namespace IQuality.Models.Chat
{
    public class PatientChat : BaseChat
    {
        public PatientChat()
        {
            GoalId = new List<string>();
        }
        
        public List<string> GoalId { get; set; }
        public string IntentName { get; set; }
        public DateTime IntentStartDate { get; set; }
        public string IntentType { get; set; }
    }
}