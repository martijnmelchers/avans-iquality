using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using IQuality.Models.Authentication.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/patient")]
    public class PatientController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IPatientService _patientService;
        public PatientController(IAsyncDocumentSession session,IPatientService patientService) : base(session)
        {
            _session = session;
            _patientService = patientService;
        }




        [HttpGet,  Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> GetPatientSettings()
        {
            var result = await _patientService.GetPatientSettings(HttpContext.User.GetUserId());
            return Ok(result);
        }

       
        [HttpPost, Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> SetPatientSettings([FromBody] PatientSettings settings)
        {
            var result = await _patientService.SetPatientSettingsAsync(settings,HttpContext.User.GetUserId());
            return Ok(result);
        }
    }
}