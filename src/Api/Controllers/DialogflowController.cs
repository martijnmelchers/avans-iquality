using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using IQuality.Api.Extensions;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Controllers
{
    [Route("/dialogflow")]
    [Injectable(interfaceType: typeof(IDialogflowService))]
    public class DialogflowController : RavenApiController
    {
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        private IDialogflowService _dialogflowService;

        public DialogflowController(IAsyncDocumentSession session, IDialogflowService dialogflowService) : base(session)
        {
            _dialogflowService = dialogflowService;
        }
        
        [HttpPost, Route("bot"), AllowAnonymous]
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
        }
        
        [HttpPost, Route("patient"), AllowAnonymous]
        public async Task<IActionResult> Set([FromBody] PatientMessage patientMessage)
        {
            QueryResult response = await _dialogflowService.ProcessClientRequest(patientMessage.text, patientMessage.roomId);
            return Json(response);
        }
    }
}

