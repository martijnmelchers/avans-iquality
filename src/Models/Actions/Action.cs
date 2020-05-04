using IQuality.Models.Goals;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Actions
{
    public class Action : IAggregateRoot
    {
        public string Id { get; set; }
        public string ChatId { get; set; }
        public ActionType Type { get; set; }
        public string Description { get; set; }
        public Interval ReminderInterval { get; set; }
    }
}
