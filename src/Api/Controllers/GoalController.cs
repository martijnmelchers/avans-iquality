using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/goals")]
    public class GoalController : RavenApiController
    {
        
        private readonly IAsyncDocumentSession _session;
        private readonly IGoalService _goalService;

        public GoalController(IAsyncDocumentSession session, IGoalService goalService) : base(session)
        {
            _session = session;
            _goalService = goalService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPatientsGoals()
        {
            return Ok(await _goalService.GetGoalsForPatient(HttpContext.User.GetUserId()));
        }

        [HttpDelete("{goalId}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> DeleteGoalByPatientId([FromRoute] string goalId)
        {
            var result = await _goalService.DeleteGoal(goalId);
            return Ok(result);
        }
    }
}