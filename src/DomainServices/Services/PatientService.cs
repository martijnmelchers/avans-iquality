using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Authentication.Settings;
using IQuality.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IPatientService))]
    public class PatientService : IPatientService
    {

        private IPatientRepository _patientRepository;


        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientSettings> GetPatientSettings(string userID)
        {
            var patient = await _patientRepository.GetPatientAsync(userID);
            return patient.Settings;
        }

        public async Task<Patient> SetPatientSettingsAsync(PatientSettings settings, string patientID)
        {
            return await _patientRepository.SetPatientSettingsAsync(settings, patientID);
        }
    }
}
