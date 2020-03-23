using IQuality.Models.Interfaces;
using System;

namespace IQuality.Models
{
    public class Measurement
    {
        public MeasurementType Type { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}