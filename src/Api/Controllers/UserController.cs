using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    
    [Route("/users")]
    public class UserController : RavenApiController
    {

        private IUserService _userService;
        public UserController(IUserService userService, IAsyncDocumentSession session) : base(session)
        {
            _userService = userService;
        }
        
        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet, Route("{applicationUserId}")]
        public async Task<IActionResult> GetUser(string applicationUserId)
        {
            return Ok(await _userService.GetUser(applicationUserId));
        }
        
        [HttpPut, Route("{applicationUserId}")]
        public async Task<IActionResult> Deactivate(string applicationUserId)
        {
            await _userService.DeactivateUser(applicationUserId);
            return Ok();
        }
        
        [HttpPut, Route("{applicationUserId}/tutorial")]
        public async Task<IActionResult> FinishedTutorial(string applicationUserId)
        {
            await _userService.FinishedTutorial(applicationUserId);
            return Ok();
        }
        
        [HttpDelete, Route("{applicationUserId}")]
        public async Task<IActionResult> Delete(string applicationUserId)
        {
            await _userService.DeleteUser(applicationUserId);
            return Ok();
        }
    }
}