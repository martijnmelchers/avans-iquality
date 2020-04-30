using IQuality.Models.Authentication;
using IQuality.Models.Authentication.Settings;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IPatientRepository : IBaseRavenRepository<Patient>
    {
        public Task<Patient> GetPatientAsync(string id);
        public Task<Patient> SetPatientSettingsAsync(PatientSettings settings, string patientID);
    }
}