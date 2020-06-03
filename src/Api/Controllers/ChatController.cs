using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/chats")]
    [Authorize]
    public class ChatController : RavenApiController
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public ChatController(IAsyncDocumentSession db, IChatService chatService, IMessageService messageService) :
            base(db)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            return Ok(await _chatService.GetChatsAsync(HttpContext.User.GetUserId()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] PatientChat chat)
        {
            string id = HttpContext.User.GetUserId();
            chat.InitiatorId = id;
            chat.CreationDate = DateTime.Now;

            return Ok(await _chatService.CreateChatAsync(chat));
        }

        [HttpPost("createbuddychat")]
        public async Task<IActionResult> CreateBuddyChat([FromBody] BuddyChat chat)
        {
            string id = HttpContext.User.GetUserId();
            chat.InitiatorId = id;
            chat.CreationDate = DateTime.Now;

            return Ok(await _chatService.CreateChatAsync(chat));
        }

        [Route("{chatId}")]
        [HttpDelete]
        public IActionResult DeleteChat(string chatId)
        {
            _chatService.DeleteChatAsync(chatId);
            return Ok();
        }

        [Route("{chatId}")]
        [HttpGet]
        public async Task<IActionResult> GetChatAsync(string chatId)
        {
            return Ok(await _chatService.GetChatAsync(chatId));
        }

        [Route("{chatId}/messages")]
        public async Task<IActionResult> GetChatMessagesPagination(string chatId, [FromQuery] int pageOffset = 1,
            [FromQuery] int pageSize = 1)
        {
            List<TextMessage> messages =
                await _messageService.GetMessages(chatId, (pageOffset - 1) * pageSize, pageSize);

            return Ok(messages);
        }

        [Route("{chatId}/messages")]
        [HttpPost]
        public async Task<IActionResult> PostChatMessage(string chatId, [FromBody] string content)
        {
            if (content == null) return NotFound();

            TextMessage messages = await _messageService.PostMessage(new TextMessage
            {
                SenderName = HttpContext.User.Claims.ToArray()[1].Value,
                ChatId = chatId,
                Content = content
            });

            return Ok(messages);
        }

        [Route("{chatId}/messages/{messageId}")]
        [HttpGet]
        public async Task<IActionResult> GetChatMessage(string chatId, string messageId)
        {
            TextMessage messages = await _messageService.GetMessage(chatId, messageId);
            return Ok(messages);
        }


        [HttpGet("{chatId}/contact")]
        public async Task<IActionResult> GetParticipant(string chatId)
        {
            ChatContext<BaseChat> chatContext = await _chatService.GetChatAsync(chatId);
            string userId = HttpContext.User.GetUserId();

            try
            {
                string contactName = await _chatService.GetContactName(userId, chatContext.Chat);
                return Ok(contactName);
            }
            catch (Exception e)
            {
                return Ok("No Contact");
            }
        }
    }
}