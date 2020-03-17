using IQuality.Models.Interfaces;

namespace IQuality.Models.Goals
{
    public class Goal : IAggregateRoot
    {
        public string Id { get; private set; }
        public GoalType Type { get; set; }
        public string Name { get; set; }
        public Interval Reminders { get; set; }
    }
}