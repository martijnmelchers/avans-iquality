using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using IQuality.Models.Authentication;
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

        [HttpPost, Route("register/buddy/{inviteToken}"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsBuddy(string inviteToken, [FromBody] BuddyRegister register)
        {
            // TODO: Validate token and use the email from the invite.
            // TODO: Consume invite?
            await _authorizationService.Register(new ApplicationUser
            {
                UserName = register.Email,
                Email = register.Email,
                Address =  register.Address,
                Name = register.Name,
            }, register.Password);

            return Ok();
        }

        [HttpPost, Route("register/patient"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsPatient()
        {
            return Ok();
        }

        [HttpPost, Route("register/doctor"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsDoctor()
        {
            return Ok();
        }

    }
}
