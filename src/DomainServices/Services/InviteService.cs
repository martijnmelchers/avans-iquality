using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class InviteService : IInviteService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInviteRepository _inviteRepository;

        public InviteService(UserManager<ApplicationUser> userManager, IInviteRepository inviteRepository)
        {
            _userManager = userManager;
            _inviteRepository = inviteRepository;
        }
        
        public async Task<Invite> CreateInvite(string userId, string email)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var inviteType = user.Roles.First() switch
            {
                Roles.Doctor => InviteType.Patient,
                Roles.Patient => InviteType.Buddy,
                Roles.Admin => InviteType.Doctor,
                _ => throw new InvalidOperationException()
            };
            

            var invite = new Invite
            {
                InviteType = inviteType,
                Token = Guid.NewGuid().ToString(),
                Email = email,
                InvitedBy = user.Id,
                Used = false
            };

            await _inviteRepository.SaveAsync(invite);
            return invite;
        }

        public async Task<Invite> GetInvite(string inviteToken)
        {
            return await _inviteRepository.GetByInviteToken(inviteToken);
        }

        // Uses the invite link.
        public async void ConsumeInvite(Invite invite)
        {
            invite.Used = true;
            await _inviteRepository.SaveAsync(invite);
        }

        public async Task<bool> ValidateInvite(string inviteToken)
        {
            var invite = await _inviteRepository.GetByInviteToken(inviteToken);
            return invite != null && !invite.Used;
        }
    }
}
