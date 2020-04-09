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
        private readonly IChatService _chatService;
        public ChatController(IAsyncDocumentSession db, IChatService chatService) : base(db)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            List<BaseChat> chat = await _chatService.GetChatsAsync();
            return Ok(chat);
        }

        [HttpPost]
        public IActionResult CreateChat([FromBody] BaseChat chat)
        {
            return Ok(_chatService.CreateChatAsync(chat));
        }

        [HttpGet]
        public IActionResult DeleteChat([FromBody] string chatId)
        {
            _chatService.DeleteChatAsync(chatId);
            return Ok();
        }

    }
}