﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuddyController : RavenApiController
    {

        private readonly IBuddyService _buddyService;

        public BuddyController(IBuddyService buddyService, IAsyncDocumentSession session) : base(session)
        {
            _buddyService = buddyService;
        }

        // GET: Buddy
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _buddyService.GetBuddies();
            return Ok(result);
        }

        // POST: Buddy/Create
        [HttpPost]
        public async Task<IActionResult> Create(Buddy buddy)
        {
            await _buddyService.AddBuddy(buddy);

            return Ok();
        }

        // POST: Buddy/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _buddyService.DeleteBuddy(id);

            return Ok();
        }
    }
}