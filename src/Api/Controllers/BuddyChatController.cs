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
using IQuality.Models.Chat;
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
        private readonly IChatService _chatService;


        public BuddyChatController(IBuddyChatService buddyGroupService, IChatService chatService, IAsyncDocumentSession session) : base(session)
        {
            _buddyGroupService = buddyGroupService;
        }

        [HttpGet("{userId}"), Authorize(Roles = Roles.Patient)]
        public async Task<IActionResult> GetBuddyChatsByUserId(string userId)
        {
            var result = await _buddyGroupService.GetBuddyChatsByUserId(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBuddyChat([FromBody] BuddyChat chat)
        {
            string id = HttpContext.User.GetUserId();
            chat.InitiatorId = id;
            chat.CreationDate = DateTime.Now;

            return Ok(await _chatService.CreateChatAsync(chat));
        }
    }
}