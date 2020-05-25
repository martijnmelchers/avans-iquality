using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;
using IQuality.Models.Measurements;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ITipRepository _tipRepository;
        private readonly IGoalRepository _goalRepository;

        public ActionService(IActionRepository actionRepository, IChatRepository chatRepository, IPatientRepository patientRepository, ITipRepository tipRepository, IGoalRepository goalRepository)
        {
            _actionRepository = actionRepository;
            _chatRepository = chatRepository;
            _patientRepository = patientRepository;
            _tipRepository = tipRepository;
            _goalRepository = goalRepository;
        }
        
        public async Task<Action> CreateAction(string chatId, string goalId, string description, string actionType)
        {
            Enum.TryParse(actionType, out ActionType type);
            var action = new Action
            {
                Type =  type,
                Description = description,
                GoalId = goalId,
                ChatId = chatId
            };

            await _actionRepository.SaveAsync(action);

            await addTipsForActionAsync(chatId, actionType);

            return action;
        }

        public async Task<string> addTipsForActionAsync(string chatId, string actionType)
        {
            var doctorApplicationUserId = await _chatRepository.GetDoctorIdFromPatientChatId(chatId);
            var patientApplicationUserId = await _chatRepository.GetPatientIdFromPatientChatId(chatId);

            if (doctorApplicationUserId != "" && patientApplicationUserId != "")
            {
                var patient = await _patientRepository.GetPatientByIdAsync(patientApplicationUserId);

                if (patient.ApplicationUserId != null && patient.DoctorId != null && patient.ApplicationUserId != "" && patient.DoctorId != "")
                {
                    var tipIdsOfAction = await _tipRepository.GetTipIdsByDoctorIdAndActionTypeAsync(doctorApplicationUserId, actionType);

                    if (tipIdsOfAction != null && tipIdsOfAction.Count != 0)
                    {
                        string firstAddedTip = "";

                        foreach (string tipId in tipIdsOfAction)
                        {
                            await _patientRepository.AddTipIdToPatient(tipId, patient.Id);
                        }

                        return firstAddedTip;
                    }
                }
            }

            return "";
        }

        public async Task<List<Action>> GetActions(string chatId)
        {
           List<Action> results  = await _actionRepository.GetAllWhereAsync(p => p.ChatId == chatId);
           return results;
        }

        public async Task<Action> SetActionReminderSettingsAsync(Interval interval, string actionId)
        {
            var result = await _actionRepository.SetActionReminderSettingsAsync(interval, actionId);

            return result;
        }

        public async Task<List<Action>> GetActionsOfPatientAsync(string userId)
        {
            if (userId != null && userId != "")
            {
                var patient = await _patientRepository.GetPatientByIdAsync(userId);

                if (patient.ApplicationUserId != null && patient.DoctorId != null && patient.ApplicationUserId != "" && patient.DoctorId != "")
                {
                    var patientChatId = await _chatRepository.GetPatientChatByPatientId(patient.ApplicationUserId);

                    var patientGoalIdsList = await _goalRepository.GetGoalIdsOfPatientByChatId(patientChatId);

                    var patientActionsList = await _actionRepository.GetActionsOfGoalIds(patientGoalIdsList);

                    return patientActionsList;
                }
            }

            return new List<Action>();
        }
    }
}