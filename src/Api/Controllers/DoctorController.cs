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
    [Route("/doctor")]
    public class DoctorController : RavenApiController
    {
        private readonly ITipService _tipService;

        public DoctorController(ITipService tipService, IAsyncDocumentSession session) : base(session)
        {
            _tipService = tipService;
        }

        [HttpGet, Route("gettips"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> GetTips()
        {
            return Ok(await _tipService.GetTipsOfDoctorAsync(HttpContext.User.GetUserId()));
        }

        [HttpGet, Route("gettipbyid/{id}"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> GetTipById(string id)
        {
            return Ok(await _tipService.GetTipByIdAsync(id));
        }

        [HttpPost, Route("createtip"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> CreateTip([FromBody] Tip tip)
        {
            if (ModelState.IsValid)
                return Ok(await _tipService.CreateTipAsync(tip, HttpContext.User.GetUserId())); else
            {
                return ValidationProblem();
            }
        }

        [HttpPut, Route("{id}"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> EditTip([FromRoute] string id, [FromBody] Tip tip)
        {
            return Ok(await _tipService.EditTipAsync(id, tip));
        }

        [HttpPost("{tipId}"), Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> DeleteTip(string tipId)
        {
            return Ok(await _tipService.DeleteTipAsync(tipId));
        }
    }
}