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
        public List<WeightMeasurement> Weight { get; set; }
        public List<BloodSugarMeasurement> BloodSugar { get; set; }
    }
}