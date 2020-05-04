using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;

        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }
        
        public async Task CreateGoal( string chatId, string description)
        {
            var goal = new Goal
            {
                ChatId = chatId,
                Description = description
            };
            
            await _goalRepository.SaveAsync(goal);
        }

        public async Task<List<Goal>> GetGoals(string chatId)
        {
            return await _goalRepository.GetGoalsOfChat(chatId);
        }

        public async Task<bool> DeleteGoal(string goalId)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            if (goal == null) return false;
            
            _goalRepository.Delete(goal);
            return true;
        }

        public async Task UpdateGoal(string goalId, string description)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            goal.Description = description;
        }

        
        public async Task<bool> GoalExists(string chatId, string description)
        {
            return await _goalRepository.GetWhereAsync(x => x.ChatId == chatId && x.Description == description) !=
                   null;
        }

        public async Task<Goal> GetGoalByDescription(string chatId, string description)
        {
            return await _goalRepository.GetWhereAsync(x => x.ChatId == chatId && x.Description == description);
        }
    }
}