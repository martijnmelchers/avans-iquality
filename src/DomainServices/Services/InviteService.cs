using System;
using System.Linq;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class InviteService : IInviteService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInviteRepository _inviteRepository;

        public async Task<Invite> CreateInvite(string userId, string email)
        {

            var user = await _userManager.FindByIdAsync(userId);

            var roles = await _userManager.GetRolesAsync(user);
            string role = roles.First();


            InviteType inviteType = InviteType.None;
            switch (role)
            {
                case "Doctor":
                    inviteType = InviteType.Patient;
                    break;
                case "Patient":
                    inviteType = InviteType.Buddy;
                    break;
            }

            if (inviteType == InviteType.None)
            {
                throw new Exception("Not a valid user");
            }

            var invite = new Invite
            {
                InviteType = inviteType,
                Token = Guid.NewGuid().ToString(),
                Email = email,
                ApplicationUserId = user.Id,
                Used = false
            };

            await _inviteRepository.SaveAsync(invite);
            return invite;
        }

        public async Task<Invite> GetInvite(string inviteToken)
        {
            return await _inviteRepository.GetWhereAsync((inv) => inv.Token.Equals(inviteToken));
        }

        // Uses the invite link.
        public async void ConsumeInvite(Invite invite)
        {
            invite.Used = true;
            await _inviteRepository.SaveAsync(invite);
        }

        public async Task<bool> ValidateInvite(string inviteToken)
        {
            var invite = await _inviteRepository.GetWhereAsync((inv => inv.Token == inviteToken));
            return invite != null;
        }
    }
}
