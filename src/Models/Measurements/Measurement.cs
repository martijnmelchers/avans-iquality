using System;
using IQuality.Models.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace IQuality.Models.Measurements
{
    public class Measurement : IAggregateRoot
    {
        public string Id { get; }
        public string PatientId { get;  }
        public MeasurementType DataType { get; }
        public double Value { get; }
        public DateTime Date { get; }

        public Measurement(string patientId, double value, MeasurementType type)
        {
            PatientId = patientId;
            Value = value;
            DataType = type;
            Date = DateTime.Now;
        }
    }
}