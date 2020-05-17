using IQuality.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Interfaces
{
    public interface ITipService
    {
        Task<List<Tip>> GetTipsOfDoctorAsync(string doctorId);
        Task<Tip> CreateTipAsync(Tip tip, string userId);
        Task<Tip> EditTipAsync(string id, Tip tip, string doctorId);
        Task<Tip> DeleteTipAsync(string tipId);
        Task<Tip> GetTipByIdAsync(string tipId);
        Task<Tip> GetRandomTipOfPatient(string patientId);
        Task<string> ConnectTipsToPatients(string tipId, string tipActionType, string doctorId);
        Task<string> DeleteTipFromPatients(string tipId, string doctorId);
    }
}
