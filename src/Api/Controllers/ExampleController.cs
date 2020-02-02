using System;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Models;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/example")]
    public class ExampleController : RavenApiController
    {
        private readonly IAsyncDocumentSession _session;
        public ExampleController(IAsyncDocumentSession session) : base(session)
        {
            _session = session;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            await _session.StoreAsync(new Example
            {
                StringValue = "Test document",
                BoolValue = false,
                NumberValue = new Random().Next(1, 10000)
            });
            return Ok("Test document created");
        }
    }
}