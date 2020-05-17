using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [ApiController]
    [Route("/buddy")]
    public class BuddyChatController : RavenApiController
    {

        private readonly IBuddyChatService _buddyGroupService;

        public BuddyChatController(IBuddyChatService buddyGroupService, IAsyncDocumentSession session) : base(session)
        {
            _buddyGroupService = buddyGroupService;
        }

        [HttpGet("{userId}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> GetBuddyChatsByUserId(string userId)
        {
            var result = await _buddyGroupService.GetBuddyChatsByUserId(userId);
            return Ok(result);
        }
    }
}