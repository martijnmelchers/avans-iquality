using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using IQuality.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/authorize")]
    public class AuthorizationController : RavenApiController
    {
        private readonly IAuthenticationService _authorizationService;

        public AuthorizationController(IAuthenticationService authorizationService, IAsyncDocumentSession session) :
            base(session)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost, Route("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var (success, user) = await _authorizationService.Login(login.Email, login.Password);
            
            if (!success)
                return Unauthorized("Invalid password and/or username");

            return Ok(_authorizationService.GenerateToken(user));
        }

        [HttpPost, Route("register"), AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return BadRequest();
        }
    }
}