using IQuality.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsOfDoctorAsync(string doctorId);
    }
}
