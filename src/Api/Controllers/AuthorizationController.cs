using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Models;
using IQuality.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide.Tcp;

namespace IQuality.Api.Controllers
{
    [Route("/authorize")]
    public class AuthorizationController : RavenApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AuthorizationController(UserManager<ApplicationUser> userManager, IAsyncDocumentSession session) : base(session)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new Login() { Email = "Nigga@chair.nl", Password = "Bruh" });
        }

        [HttpPost, Route("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            


            return Ok();
        }

        [HttpPost, Route("register"), AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return BadRequest();
        }
    }
}