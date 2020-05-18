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
        private readonly IChatService _chatService;

        public InviteService(UserManager<ApplicationUser> userManager, IInviteRepository inviteRepository, IChatService chatService)
        {
            _userManager = userManager;
            _inviteRepository = inviteRepository;
            _chatService = chatService;
        }
        
        public async Task<Invite> CreateInvite(string userId, string email, string chatId = "")
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
                ChatId = chatId
            };
            
            await _inviteRepository.SaveAsync(invite);
            
            return invite;
        }

        public async Task<Invite> GetInvite(string inviteToken)
        {
            return await _inviteRepository.GetByInviteToken(inviteToken);
        }

        // Uses the invite link.
        public async Task<string> ConsumeInvite(string inviteToken, string applicationUserId)
        {
            var invite = await _inviteRepository.GetByInviteToken(inviteToken);
            invite.Consume();
            
            if (invite.ChatId != null)
            {
                await _chatService.AddUserToChat(applicationUserId, invite.ChatId);
                return invite.ChatId;
            }
            
            return null;
        }

        public async Task<bool> ValidateInvite(string inviteToken)
        {
            var invite = await _inviteRepository.GetByInviteToken(inviteToken);
            return invite != null && !invite.Consumed;
        }
    }
}
