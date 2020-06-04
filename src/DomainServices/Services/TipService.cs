using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Doctor;
using IQuality.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(ITipService))]
    public class TipService : ITipService
    {
        private readonly ITipRepository _tipRepository;
        private readonly IActionRepository _actionRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IChatRepository _chatRepository;

        public TipService(ITipRepository tipRepository, IActionRepository actionRepository, IPatientRepository patientRepository, IGoalRepository goalRepository, IChatRepository chatRepository)
        {
            _tipRepository = tipRepository;
            _actionRepository = actionRepository;
            _patientRepository = patientRepository;
            _goalRepository = goalRepository;
            _chatRepository = chatRepository;
        }

        public async Task<Tip> CreateTipAsync(Tip tip, string doctorId)
        {
            tip.DoctorId = doctorId;

            var createdTip = await _tipRepository.CreateTipAsync(tip);
            
            await ConnectTipsToPatients(createdTip.Id, createdTip.ActionType, doctorId);

            return createdTip;
        }

        public async Task<Tip> EditTipAsync(string id, Tip tip, string doctorId)
        {
            tip.Id = id;

            var edittedTip = await _tipRepository.EditTipAsync(tip);

            await ConnectTipsToPatients(id, tip.ActionType, doctorId);

            return edittedTip;
        }

        public async Task<Tip> DeleteTipAsync(string tipId)
        {
            return await _tipRepository.DeleteTipAsync(tipId);
        }

        public Task<List<Tip>> GetTipsOfDoctorAsync(string doctorId)
        {
            return _tipRepository.GetTipsOfDoctorAsync(doctorId);
        }

        public async Task<Tip> GetTipByIdAsync(string id)
        {
            return await _tipRepository.GetTipByIdAsync(id);
        }

        public async Task<string> ConnectTipsToPatients(string tipId, string tipActionType, string doctorId)
        {
            var patients = await _patientRepository.GetAllPatientsOfDoctorAsync(doctorId);

            foreach (Patient patient in patients)
            {
                var patientChatId = await _chatRepository.GetPatientChatByPatientId(patient.ApplicationUserId);

                var patientGoalIdsList = await _goalRepository.GetGoalIdsOfPatientByChatId(patientChatId);

                var patientActionTypesList = await _actionRepository.GetActionTypesOfGoalIds(patientGoalIdsList);

                foreach (string actionType in patientActionTypesList)
                {
                    if (patient.TipIds == null)
                    {
                        await _patientRepository.InitializeTipIdsList(patient.Id);
                    }
                    if (tipActionType == actionType && !patient.TipIds.Contains(tipId))
                    {
                        await _patientRepository.AddTipIdToPatient(tipId, patient.Id);
                    }
                }
            }

            return doctorId;
        }

        public async Task<string> DeleteTipFromPatients(string tipId, string doctorId)
        {
            var patients = await _patientRepository.GetAllPatientsOfDoctorAsync(doctorId);

            foreach (Patient patient in patients)
            {
                if (patient.TipIds.Contains(tipId))
                {
                    await _patientRepository.DeleteTipIdFromPatient(tipId, patient.Id);
                }
            }

            return doctorId;
        }

        public async Task<Tip> GetRandomTipOfPatient(string patientId)
        {
            var randomTipId = await _patientRepository.GetRandomTipIdFromPatient(patientId);

            if (randomTipId != "-1")
            {
                return await _tipRepository.GetTipByIdAsync(randomTipId);
            }

            return new Tip();   // returns tip with Id = null, which can be checked for in the frontend.
        }
    }
}
