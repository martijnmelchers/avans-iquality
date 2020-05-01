using IQuality.Models.Interfaces;

namespace IQuality.Models.Actions
{
    public abstract class BaseAction : IAggregateRoot
    {
        public string Id { get; set; }
    }
}
