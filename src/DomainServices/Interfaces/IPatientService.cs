using IQuality.Models.Authentication;
using IQuality.Models.Authentication.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
   public interface IPatientService
    {
        Task<PatientSettings> GetPatientSettings(string userID);
        public Task<Patient> SetPatientSettingsAsync(PatientSettings settings, string patientID);
    }
}
