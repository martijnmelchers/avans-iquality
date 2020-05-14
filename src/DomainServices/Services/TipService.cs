using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
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

        public TipService(ITipRepository tipRepository, IActionRepository actionRepository)
        {
            _tipRepository = tipRepository;
            _actionRepository = actionRepository;
        }

        public async Task<Tip> CreateTipAsync(Tip tip, string userId)
        {
            tip.UserId = userId;

            return await _tipRepository.CreateTipAsync(tip);
        }

        public async Task<Tip> EditTipAsync(string id, Tip tip)
        {
            tip.Id = id;

            return await _tipRepository.EditTipAsync(tip);
        }

        public async Task<Tip> DeleteTipAsync(string tipId)
        {
            return await _tipRepository.DeleteTipAsync(tipId);
        }

        public Task<List<Tip>> GetTipsOfDoctorAsync(string userId)
        {
            return _tipRepository.GetTipsOfDoctorAsync(userId);
        }
    }
}
