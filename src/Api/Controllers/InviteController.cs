using System;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    public class InviteData
    {
        public string Email { get; set; }
        public string ChatId { get; set; }
    }

    [Route("/invite")]
    public class InviteController : RavenApiController
    {
        private readonly IInviteService _inviteService;

        public InviteController(IAsyncDocumentSession session, IInviteService inviteService) : base(session)
        {
            _inviteService = inviteService;
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
            try
            {
                var invite =
                    await _inviteService.CreateInvite(HttpContext.User.GetUserId(), data.Email, data.ChatId);
                
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