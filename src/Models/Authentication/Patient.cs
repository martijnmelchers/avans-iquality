using System.Collections.Generic;
using IQuality.Models.Authentication.Settings;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Authentication
{
    public class Patient : IAggregateRoot
    {
        public string Id { get; private set; }
        public string ApplicationUserId { get; set; }
        public string DoctorId { get; set; }
        public PatientSettings Settings { get; set; }
        public List<string> TipIds { get; set; }
        public List<string> NotificationIds { get; set; }

        public Patient(string applicationUserId, string doctorId)
        {
            ApplicationUserId = applicationUserId;
            DoctorId = doctorId;
        }
    }
}