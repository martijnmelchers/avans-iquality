using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/chats"), Authorize]
    public class ChatController : RavenApiController
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        
        public ChatController(IAsyncDocumentSession db, IChatService chatService, IMessageService messageService) : base(db)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            List<BaseChat> chat = await _chatService.GetChatsAsync();
            return Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] PatientChat chat)
        {
            string id = HttpContext.User.GetUserId();
            chat.InitiatorId = id;
            chat.CreationDate = DateTime.Now;
            
            BaseChat createdChat = await _chatService.CreateChatAsync(chat);
            return Ok(createdChat);
        }

        
 
        [Route("/{chatId}"), HttpDelete]
        public IActionResult DeleteChat(string chatId)
        {
            _chatService.DeleteChatAsync(chatId);
            return Ok();
        }

        [Route("/{chatId}"), HttpGet]
        public async Task<IActionResult> GetChatAsync(string chatId)
        {
            BaseChat result =  await _chatService.GetChatAsync(chatId);
            return Ok(result);
        }
        
        [Route("/{chatId}/messages"), HttpGet]
        public async Task<OkObjectResult> GetChatMessages(string chatId)
        {
            List<TextMessage> messages = await _messageService.GetMessages(chatId);
            return Ok(messages);
        }
        
        [Route("/{chatId}/messages"), HttpPost]
        public async Task<IActionResult> PostChatMessage(string chatId, [FromBody] string content)
        {
            if (content == null) return NotFound();


            TextMessage messages = await _messageService.PostMessage(new TextMessage()
            {
                ChatId = chatId,
                Content = content
            });
            return Ok(messages);
        }
        
        [Route("/{chatId}/messages/{messageId}"), HttpGet]
        public async Task<IActionResult> GetChatMessage(string chatId, string messageId)
        {
            TextMessage messages = await _messageService.GetMessage(chatId, messageId);
            return Ok(messages);
        }
    }
}