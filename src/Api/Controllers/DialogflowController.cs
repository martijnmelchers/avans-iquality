using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.DomainServices.Interfaces;
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
        private readonly IGoalService _goalService;
        private IDialogflowService _dialogflowService;

        public DialogflowController(IGoalService goalService, IAsyncDocumentSession session, IDialogflowService dialogflowService) : base(session)
        {
            _goalService = goalService;
            _dialogflowService = dialogflowService;
        }

        [HttpPost, Route("patient"), Authorize]
        public async Task<IActionResult> Set([FromBody] TextMessage textMessage)
        {
            return Ok(await _dialogflowService.ProcessClientRequest(textMessage.Content, textMessage.ChatId, HttpContext.User.GetUserId() ));
        }

        
        //TODO @Huseyin verplaatsen naar GoalController niet DialogflowController!
        [HttpDelete, Route("goal/{goalId}"), Authorize]
        public async Task<IActionResult> DeleteGoal(string goalId)
        {
            if (await _goalService.DeleteGoal(goalId))
            {
                return Ok("Goal has been deleted");
            }

            return NotFound("Could Not Find the Goal!");
        }
    }
}

