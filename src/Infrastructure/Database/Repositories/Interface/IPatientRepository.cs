using IQuality.Models.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IPatientRepository : IBaseRavenRepository<Patient>
    {
        Task<List<PatientApplicationUser>> GetAllPatientsOfDoctorAsync(string doctorId);
    }
}