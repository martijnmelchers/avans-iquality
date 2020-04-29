using System.Collections.Generic;
using IQuality.Models.Actions;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Goals
{
    public class Goal : IAggregateRoot
    {
        public string Id { get; private set; }
        public GoalType Type { get; set; }
        public string Description { get; set; }
        public Interval Reminders { get; set; }
        
        public List<BaseAction> Actions { get; set; }
    }
}