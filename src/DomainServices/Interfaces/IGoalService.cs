using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;
using IQuality.Models.Goals;

namespace IQuality.DomainServices.Interfaces
{
    public interface IGoalService
    {
        Task SaveGoal(string goalDescription, PatientChat chat);
        Task<List<Goal>> GetGoals(PatientChat chat);
    }
}