using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<Patient>> GetAllPatientsOfDoctorAsync(string doctorId)
        {
            return await _patientRepository.GetAllPatientsOfDoctorAsync(doctorId);
        }

        public async Task<List<string>> SetPatientNotificationIdAsync(string notificationId, string isSubscribed, string patientId)
        {
            bool isSubscribedBoolean = false;
            bool.TryParse(isSubscribed, out isSubscribedBoolean);

            return await _patientRepository.SetPatientNotificationIdAsync(notificationId, isSubscribedBoolean, patientId);
        }
    }
}
