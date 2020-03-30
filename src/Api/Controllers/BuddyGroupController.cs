using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuddyGroupController : RavenApiController
    {

        private IBuddyGroupRepository _buddyGroupRepository;
        private readonly IAsyncDocumentSession _session;

        public BuddyGroupController(IBuddyGroupRepository buddyGroupRepository, IAsyncDocumentSession session) : base(session)
        {
            _buddyGroupRepository = buddyGroupRepository;
            _session = session;
        }

        // GET: BuddyGroup
        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var result = await _buddyGroupRepository.GetAll();
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        // GET: BuddyGroup/Details/5
        [HttpGet("[action]")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BuddyGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuddyGroup/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(Buddy buddy)
        {
            var result = await _buddyGroupRepository.AddBuddy(buddy);

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        // GET: BuddyGroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BuddyGroup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BuddyGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BuddyGroup/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}