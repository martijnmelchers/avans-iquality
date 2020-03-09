using IQuality.Models.Interfaces;

namespace IQuality.Models
{
    public class Patient : IAggregateRoot
    {
        public string Id { get; private set; }
        public string ApplicationUserId { get; set; }
        public string DoctorId { get; set; }
        public PatientSettings Settings { get; set; }
    }
}