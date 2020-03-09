using IQuality.Models.Interfaces;

namespace IQuality.Models.Goals
{
    public abstract class BaseGoal : IAggregateRoot
    {
        public string Id { get; private set; }
        public string Title { get; set; }
        public Interval Reminders { get; set; }
    }
}