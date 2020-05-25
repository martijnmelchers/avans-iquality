using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Models.Authentication;
using IQuality.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/actiontypes")]
    [ApiController]
    public class ActionTypesController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IActionTypesService _actionTypesService;

        public ActionTypesController(IAsyncDocumentSession session, IActionTypesService actionTypesService) : base(session)
        {
            _session = session;
            _actionTypesService = actionTypesService;
        }

        [HttpGet, Authorize(Roles = "patient, doctor")]
        public IActionResult GetActionTypes()
        {
            return Ok(_actionTypesService.GetActionTypes());
        }
    }
}