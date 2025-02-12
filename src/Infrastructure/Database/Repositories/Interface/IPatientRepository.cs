using IQuality.Models.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IPatientRepository : IBaseRavenRepository<Patient>
    {
        Task<List<Patient>> GetAllPatientsOfDoctorAsync(string doctorId);
        Task<List<string>> AddTipIdToPatient(string tipId, string patientId);
        Task<List<string>> DeleteTipIdFromPatient(string tipId, string patientId);
        Task<List<string>> InitializeTipIdsList(string patientId);
        Task<string> GetRandomTipIdFromPatient(string patientId);
        Task<List<string>> SetPatientNotificationIdAsync(string notificationId, bool isSubscribed, string patientId);
        Task<Patient> GetPatientByIdAsync(string id);
    }
}