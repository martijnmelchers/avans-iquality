using System;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Linq;
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
    }
}