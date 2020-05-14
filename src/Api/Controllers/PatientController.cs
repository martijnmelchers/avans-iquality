using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Authentication.Settings;
using IQuality.Models.Chat;
using IQuality.Models.Goals;
using IQuality.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/patient")]
    public class PatientController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IActionService _actionService;

        public PatientController(IAsyncDocumentSession session, IActionService actionService) : base(session)
        {
            _session = session;
            _actionService = actionService;
        }

        // setactionreminder duratie actionid
        [HttpPost("setactionremindersettings/{actionId}/{interval}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> SetActionReminderSettings([FromRoute] Interval interval, [FromRoute] string actionId)
        {
            var result = await _actionService.SetActionReminderSettingsAsync(interval, actionId);

            return Ok(result);
        }
    }
}