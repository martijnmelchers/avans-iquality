using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Authentication;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/dialogflow")]
    [Injectable(interfaceType: typeof(IDialogflowService))]
    public class DialogflowController : RavenApiController
    {
        // private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        private readonly IGoalIntentHandler _goalIntentHandler;
        private IDialogflowService _dialogflowService;

        public DialogflowController(IGoalIntentHandler goalIntentHandler, IAsyncDocumentSession session, IDialogflowService dialogflowService) : base(session)
        {
            _goalIntentHandler = goalIntentHandler;
            _dialogflowService = dialogflowService;
        }
        
        /*[HttpPost, Route("bot"), AllowAnonymous]
        public async Task<IActionResult> Set()
        {
            WebhookRequest request;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string lines = await reader.ReadToEndAsync();
                request = jsonParser.Parse<WebhookRequest>(lines);
            }
            
            await _dialogflowService.ProcessWebhookRequest(request);
            return Ok();
        }*/
        
        [HttpPost, Route("patient"), Authorize]
        public async Task<IActionResult> Set([FromBody] TextMessage textMessage)
        {
            return Ok(await _dialogflowService.ProcessClientRequest(textMessage.Content, textMessage.ChatId, HttpContext.User.GetUserId() ));
        }

        [HttpDelete, Route("goal/{goalId}"), Authorize]
        public async Task<IActionResult> DeleteGoal(string goalId)
        {
            if (await _goalIntentHandler.DeleteGoal(goalId))
            {
                return Ok();
            }

            return BadRequest("Something went wrong while deleting the Goal");
        }
    }
}

