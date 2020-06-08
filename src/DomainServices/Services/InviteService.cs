using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class InviteService : IInviteService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInviteRepository _inviteRepository;
        private readonly IChatService _chatService;
        private SendGridClient _client;
        public InviteService(UserManager<ApplicationUser> userManager, IInviteRepository inviteRepository, IChatService chatService)
        {
            _userManager = userManager;
            _inviteRepository = inviteRepository;
            _chatService = chatService;
            
            _client = new SendGridClient("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");
        }
        
        public async Task<Invite> CreateInvite(string userId, string email, string chatId = "", string chatName = "")
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            var inviteType = user.Roles.First() switch
            {
                Roles.Doctor => InviteType.Patient,
                Roles.Patient => InviteType.Buddy,
                Roles.Admin => InviteType.Doctor,
                _ => throw new InvalidOperationException()
            };
            
            if (chatName != null)
            {
                ChatContext<BaseChat> chatContext = null;
                
                switch (inviteType)
                {
                    case InviteType.Admin:
                        break;
                    case InviteType.Buddy:
                        chatContext = await this.CreateChat<BuddyChat>(chatName, user.Id);
                        break;
                    case InviteType.Doctor:
                        break;
                    case InviteType.Patient:
                        chatContext = await this.CreateChat<PatientChat>(chatName, user.Id);
                        break;
                }

                if (chatContext != null)
                {
                    chatId = chatContext.Chat.Id;
                }
            }

            var invite = new Invite
            {
                InviteType = inviteType,
                Token = Guid.NewGuid().ToString(),
                Email = email,
                InvitedBy = user.Email,
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
        
        private async Task<ChatContext<BaseChat>> CreateChat<T>(string chatName, string userId) where T: BaseChat, new()
        {

            var item = new T()
            {
                Name = chatName,
                InitiatorId = userId,
                ParticipatorIds = new List<string>(){}
            };
            
            return await _chatService.CreateChatAsync(item);
        }

        public async Task SendInviteEmail(Invite inv)
        {
            var from = new EmailAddress("saschamendel2016@gmail.com");
            var to = new EmailAddress(inv.Email);
            var subject = "Your invite from iquality";
            var htmlContent = String.Format("<a href='http://localhost:4200/invite/{0}'>Click here to open your invite.</a>", inv.Token);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = await _client.SendEmailAsync(msg);
        }
    }
}
