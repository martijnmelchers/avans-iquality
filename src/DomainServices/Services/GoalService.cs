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
        private readonly IChatRepository _chatRepository;
        private readonly IPatientRepository _patientRepository;

        public GoalService(IGoalRepository goalRepository, IChatRepository chatRepository, IPatientRepository patientRepository)
        {
            _goalRepository = goalRepository;
            _chatRepository = chatRepository;
            _patientRepository = patientRepository;
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

        public async Task<List<Goal>> GetGoalsForPatient(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return new List<Goal>();
            
            
            var patient = await _patientRepository.GetPatientByIdAsync(userId);

            if (patient.ApplicationUserId == null || patient.DoctorId == null || patient.ApplicationUserId == "" ||
                patient.DoctorId == "") return new List<Goal>();
            
            string patientChatId = await _chatRepository.GetPatientChatByPatientId(patient.ApplicationUserId);

            List<Goal> patientGoalList = await _goalRepository.GetGoalsOfChat(patientChatId);
                    
            return patientGoalList;

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