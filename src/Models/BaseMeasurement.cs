using IQuality.Models.Interfaces;

namespace IQuality.Models
{
    public abstract class BaseMeasurement : IAggregateRoot
    {
        public string Id { get; set; }
    }
}