using System;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    public class InviteData
    {
        public string Email { get; set; }
        public string ChatId { get; set; }

        public string ChatName { get; set; } = null;
    }

    [Route("/invite")]
    public class InviteController : RavenApiController
    {
        private readonly IInviteService _inviteService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IChatService _chatService;

        public InviteController(IAsyncDocumentSession session, IInviteService inviteService, IAuthenticationService authenticationService, IChatService chatService) : base(session)
        {
            _inviteService = inviteService;
            _authenticationService = authenticationService;
            _chatService = chatService;
        }

        [HttpGet, Route("{inviteToken}")]
        public async Task<IActionResult> GetInvite(string inviteToken)
        {
            var invite = await _inviteService.GetInvite(inviteToken);
            if (invite == null)
                return NotFound();

            return Ok(invite);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvite([FromBody] InviteData data)
        {
            if (await _authenticationService.ApplicationUserExists(data.Email))
                return Conflict("The user that you're trying to invite already exists.");
            
            try
            {

                var invite =
                    await _inviteService.CreateInvite(HttpContext.User.GetUserId(), data.Email, data.ChatId, data.ChatName);
                
                
                await _inviteService.SendInviteEmail(invite);
                return Ok(invite);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }
    }
}