using System;
using IQuality.Models.Interfaces;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace IQuality.Models.Measurements
{
    public class Measurement : IAggregateRoot
    {
        public string Id { get; }
        public string PatientId { get; }
        public MeasurementType DataType { get; }
        public double Value { get; }
        public DateTime Date { get; }


        [JsonConstructor]
        public Measurement(string id, string patientId, MeasurementType type, double value, DateTime date)
        {
            Id = id;
            PatientId = patientId;
            DataType = type;
            Value = value;
            Date = date;
        }

        public Measurement(string patientId, double value, MeasurementType type)
        {
            PatientId = patientId;
            Value = value;
            DataType = type;
            Date = DateTime.Now;
        }
    }
}