using System;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Actions;
using IQuality.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/{controller}")]
    public class ReminderController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        private readonly IReminderService _reminderService;

        public ReminderController(IAsyncDocumentSession session, IReminderService reminderService) : base(session)
        {
            _session = session;
            _reminderService = reminderService;
        }

        [HttpGet("alloftodaysreminders"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> GetAllRemindersOfToday()
        {
            var userId = HttpContext.User.GetUserId();

            var remindersList = await _reminderService.GetAllRemindersOfTodayAsync(userId);

            return Ok(remindersList);
        }

        [HttpGet("todaysreminders"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> GetRemindersOfToday()
        {
            var userId = HttpContext.User.GetUserId();

            var remindersList = await _reminderService.GetRemindersOfTodayAsync(userId);

            return Ok(remindersList);
        }

        [HttpPost, Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> CreateReminder([FromBody] string actionDescription)
        {
            var userId = HttpContext.User.GetUserId();
            string todaysDate = DateTime.Today.ToString();

            Reminder reminder = new Reminder
            {
                ActionDescription = actionDescription,
                UserId = userId,
                Date = todaysDate
            };

            var createdReminder = await _reminderService.CreateReminderAsync(reminder);

            return Ok(createdReminder);
        }
    }
}