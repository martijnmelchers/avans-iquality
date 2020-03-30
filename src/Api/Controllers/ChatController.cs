using System.Threading.Tasks;
using IQuality.Api.Extensions;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/chats")]
    public class ChatController : RavenApiController
    {
        private readonly ChatRepository _repository;
        public ChatController(IAsyncDocumentSession db, ChatRepository repository) : base(db)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetChats()
        {
            return Ok(_repository.GetChats());
        }

        [HttpPost]
        public IActionResult CreateChat([FromBody] BaseChat chat)
        {
            return Ok(_repository.SaveAsync(chat));
        }

        public async Task<OkResult> DeleteChat([FromBody] string chatId)
        {
            var chat = await _repository.GetByIdAsync(chatId);
            await _repository.DeleteAsync(chat);
            
            return Ok();
        }
    }
}