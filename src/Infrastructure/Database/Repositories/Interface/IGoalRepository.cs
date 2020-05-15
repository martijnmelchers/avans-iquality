using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Goals;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IGoalRepository : IBaseRavenRepository<Goal>
    {
        Task SaveAsyncCheckDescription(string description, string roomId);
        Task<Goal> GetWhereDescription(string description);
        Task<List<Goal>> GetGoalsOfChat(string chatId);
        Task<List<string>> GetGoalIdsOfPatient(string patientId);
    }
}