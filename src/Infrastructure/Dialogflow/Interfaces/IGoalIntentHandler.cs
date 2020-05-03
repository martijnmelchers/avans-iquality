using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat;
using IQuality.Models.Goals;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IGoalIntentHandler : IIntentHandler
    {
        Task SaveGoal(string goalDescription, PatientChat chat);
        Task<List<Goal>> GetGoals(PatientChat chat);
        Task DeleteGoal(string goalId);
    }
}