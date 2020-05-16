using System;
using IQuality.Models.Interfaces;

namespace IQuality.Models.PatientData
{
    public class PatientData: IAggregateRoot
    {
        public string PatientId { get; set; }
        public string Id { get; set; }
        
        public string Xtitle { get;  set; }
        public string Ytitle { get;  set; }
        
        public string DataType { get; set; }
        public float Value { get; set; }
        public float Surplus { get; set; }
        public string Group { get; set; }
        public DateTime Date { get; set; }
    }
}