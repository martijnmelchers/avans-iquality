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
        private readonly IActionTypesService _actionTypesService;

        public ActionController(IAsyncDocumentSession session, IActionService actionService, IActionTypesService actionTypesService) : base(session)
        {
            _session = session;
            _actionService = actionService;
            _actionTypesService = actionTypesService;
        }

        [HttpGet, Authorize(Roles = "Patient, Doctor")]
        public IActionResult GetActionTypes()
        {
            return Ok(_actionTypesService.GetActionTypes());
        }

        // setactionreminder duratie actionid
        [HttpPost("{actionId}/{interval}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> SetActionReminderSettings([FromRoute] Interval interval, [FromRoute] string actionId)
        {
            var result = await _actionService.SetActionReminderSettingsAsync(interval, actionId);

            return Ok(result);
        }
    }
}