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

        [HttpPost]
        public async Task<IActionResult> CreateInvite([FromBody] string email)
        {
            var invite = await _inviteService.CreateInvite(HttpContext.User.GetUserId(), email);
            return Ok(invite);
        }
    }
}
