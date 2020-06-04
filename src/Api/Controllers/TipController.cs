using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using IQuality.Models.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/tip")]
    public class TipController : RavenApiController
    {
        private readonly ITipService _tipService;

        public TipController(ITipService tipService, IAsyncDocumentSession session) : base(session)
        {
            _tipService = tipService;
        }

        [HttpGet, Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> GetTips()
        {
            return Ok(await _tipService.GetTipsOfDoctorAsync(HttpContext.User.GetUserId()));
        }

        [HttpGet, Route("{tipId}"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> GetTipById(string tipId)
        {
            return Ok(await _tipService.GetTipByIdAsync(tipId));
        }

        [HttpGet, Route("getrandomtip")]
        public async Task<IActionResult> GetRandomTip()
        {
            return Ok(await _tipService.GetRandomTipOfPatient(HttpContext.User.GetUserId()));
        }

        [HttpPost, Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> CreateTip([FromBody] Tip tip)
        {
            if (ModelState.IsValid)
                return Ok(await _tipService.CreateTipAsync(tip, HttpContext.User.GetUserId())); else
            {
                return ValidationProblem();
            }
        }

        [HttpPut, Route("{tipId}"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> EditTip([FromRoute] string tipId, [FromBody] Tip tip)
        {
            return Ok(await _tipService.EditTipAsync(tipId, tip, HttpContext.User.GetUserId()));
        }

        [HttpDelete, Route("{tipId}"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> DeleteTip(string tipId)
        {
            return Ok(await _tipService.DeleteTipAsync(tipId));
        }
    }
}