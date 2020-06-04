using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Goals;

namespace IQuality.DomainServices.Interfaces
{
    public interface IGoalService
    {
        Task CreateGoal(string chatId, string description);
        Task<List<Goal>> GetGoals(string chatId);
        Task<List<Goal>> GetGoalsForPatient(string userId);
        Task<bool> DeleteGoal(string goalId);
        Task UpdateGoal(string goalId, string description);
        Task<bool> GoalExists(string chatId, string description);
        Task<Goal> GetGoalByDescription(string chatId, string userInput);
    }
}