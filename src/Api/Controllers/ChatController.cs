using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/chats"), Authorize]
    public class ChatController : RavenApiController
    {
        private readonly IChatService _service;
        public ChatController(IAsyncDocumentSession db, IChatService service) : base(db)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            List<BaseChat> chat = await _service.GetChatsAsync();
            return Ok(chat);
        }

        [HttpPost]
        public IActionResult CreateChat([FromBody] BaseChat chat)
        {
            return Ok(_service.CreateChatAsync(chat));
        }

        public IActionResult DeleteChat([FromBody] string chatId)
        {
            _service.DeleteChatAsync(chatId);
             return Ok();
        }
    }
}