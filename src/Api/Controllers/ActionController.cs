using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/action")]
    public class ActionController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IActionService _actionService;

        public ActionController(IAsyncDocumentSession session, IActionService actionService) : base(session)
        {
            _session = session;
            _actionService = actionService;
        }

        [HttpGet, Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> GetPatientsActions()
        {
            return Ok(await _actionService.GetActionsOfPatientAsync(HttpContext.User.GetUserId()));
        }

        // setactionreminder duratie actionid
        [HttpPost("{actionId}/{interval}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> SetActionReminderSettings([FromRoute] Interval interval, [FromRoute] string actionId)
        {
            var result = await _actionService.SetActionReminderSettingsAsync(interval, actionId);

            return Ok(result);
        }
        
        
        [HttpDelete("{goalId}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> SetActionReminderSettings([FromRoute] string goalId)
        {
            _actionService.DeleteActionsForGoal(goalId);
            return Ok();
        }
    }
}