using System;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IQuality.Api.Controllers
{
    [Route("/invite")]
    public class InviteController : Controller
    {
        private IInviteService _inviteService;

        public InviteController(IInviteService inviteService)
        {
            _inviteService = inviteService;
        }

        [HttpGet]
        [Route("/{inviteToken}")]
        public async Task<IActionResult> GetInvite(string inviteToken)
        {
            var invite = await _inviteService.GetInvite(inviteToken);
            if (invite == null)
                return NotFound();

            return Ok(invite);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvite([FromBody] string email, [FromBody] string groupName)
        {
            try
            {
                var invite = await _inviteService.CreateInvite(HttpContext.User.GetUserId(), email, groupName);
                return Ok(invite);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }
    }
}
