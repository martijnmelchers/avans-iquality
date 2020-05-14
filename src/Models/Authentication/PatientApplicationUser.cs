using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models.Authentication
{
    public class PatientApplicationUser : IAggregateRoot
    {
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string DoctorId { get; set; }
    }
}
