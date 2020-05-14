using System.Collections.Generic;
using IQuality.Models.Authentication.Settings;
using IQuality.Models.Interfaces;
using IQuality.Models.Measurements;

namespace IQuality.Models.Authentication
{
    public class Patient : IAggregateRoot
    {
        public string Id { get; private set; }
        public string ApplicationUserId { get; set; }
        public string DoctorId { get; set; }
        public PatientSettings Settings { get; set; }
        public List<string> TipIds { get; set; }

        // Contains the measurements
        public List<Measurement> Weight { get; set; }
        public List<Measurement> BloodSugar { get; set; }
        public List<Measurement> BloodPressure { get; set; }
        public List<Measurement> Cholesterol { get; set; }

        public Patient(string applicationUserId, string doctorId)
        {
            ApplicationUserId = applicationUserId;
            DoctorId = doctorId;
        }
    }
}