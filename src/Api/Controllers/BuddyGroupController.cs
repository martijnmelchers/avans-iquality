using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuddyGroupController : RavenApiController
    {

        private readonly IBuddyGroupService _buddyGroupService;

        public BuddyGroupController(IBuddyGroupService buddyGroupService, IAsyncDocumentSession session) : base(session)
        {
            _buddyGroupService = buddyGroupService;
        }



        // GET: BuddyGroup
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _buddyGroupService.GetBuddygroupNames();
            return Ok(result);
        }

        //// POST: BuddyGroup/Create
        //[HttpPost]
        //public async Task<IActionResult> Create(Buddy buddy)
        //{
        //    await _buddyGroupService.AddBuddy(buddy);

        //    return Ok();
        //}

        //// POST: BuddyGroup/Delete/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _buddyGroupService.DeleteBuddy(id);

        //    return Ok();
        //}
    }
}