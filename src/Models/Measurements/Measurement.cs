using System;

namespace IQuality.Models.Measurements
{
    public class Measurement
    {
        public MeasurementType Type { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}