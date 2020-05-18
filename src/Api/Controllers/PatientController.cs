using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/patient")]
    public class PatientController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IPatientService _patientService;

        public PatientController(IAsyncDocumentSession session, IPatientService patientService) : base(session)
        {
            _session = session;
            _patientService = patientService;
        }

        [HttpGet, Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> GetAllPatients()
        {
            return Ok(await _patientService.GetAllPatientsOfDoctorAsync(HttpContext.User.GetUserId()));
        }

        [HttpPost, Route("{notificationId}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> SetPatientNotificationId([FromRoute] string notificationId)
        {
            return Ok(await _patientService.SetPatientNotificationIdAsync(notificationId, HttpContext.User.GetUserId()));
        }
    }
}
