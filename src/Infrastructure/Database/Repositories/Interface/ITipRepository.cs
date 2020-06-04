using IQuality.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface ITipRepository : IBaseRavenRepository<Tip>
    {
        Task<List<Tip>> GetTipsOfDoctorAsync(string doctorId);
        Task<Tip> CreateTipAsync(Tip tip);
        Task<Tip> EditTipAsync(Tip edittedTip);
        Task<Tip> DeleteTipAsync(string tipId);
        Task<Tip> GetTipByIdAsync(string tipId);
        Task<List<string>> GetTipIdsByDoctorIdAndActionTypeAsync(string doctorId, string actionType);
    }
}
