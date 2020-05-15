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
        Task<Tip> EditTipAsync(string id, Tip tip);
        Task<Tip> DeleteTipAsync(string tipId);
    }
}
