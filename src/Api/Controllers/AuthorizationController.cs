using System;
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
        
        [HttpPost, Route("register/{inviteToken}"), AllowAnonymous]
        public async Task<IActionResult> RegisterAsPatient(string inviteToken, [FromBody] UserRegister register)
        {
            try
            {
                (string chatId, ApplicationUser user) userData = await _authorizationService.Register(inviteToken, register);
                return Ok(userData);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpGet, Route("verifyToken"), Authorize]
        public IActionResult VerifyToken()
        {
            return Ok("Seems to me like you're logged in!");
        }
    }
}
