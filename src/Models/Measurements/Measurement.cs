using System;

namespace IQuality.Models.Measurements
{
    public class Measurement
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }

        public Measurement(double value)
        {
            Date = DateTime.Now;
            Value = value;
        }
    }
}