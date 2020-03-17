using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace IQuality.Models
{
    public class Patient : IAggregateRoot
    {
        public string Id { get; private set; }
        public string ApplicationUserId { get; set; }
        public string DoctorId { get; set; }
        public PatientSettings Settings { get; set; }

        // Contains the measurements
        public List<Measurement> Weight { get; set; }
        public List<Measurement> BloodSugar { get; set; }
        public List<Measurement> BloodPressure { get; set; }
        public List<Measurement> Cholesterol { get; set; }
    }
}