using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Helpers;

namespace IQuality.Api.Controllers
{
    public class myModel
    {
        public string Text { get; set; }
        public QueryResult Response { get; set; }
    }
        
    [Route("/dialogflow")]
    [Injectable(interfaceType: typeof(IDialogflowService))]
    public class DialogflowController : Controller
    {
        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        private IDialogflowService _dialogflowService;

        public DialogflowController(IDialogflowService dialogflowService)
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
            
            
            _dialogflowService.ProcessWebhookRequest(request);
            return Ok();
        }
        
        [HttpPost, Route("patient"), AllowAnonymous]
        public IActionResult Set([FromBody] myModel result)
        {
            QueryResult response = _dialogflowService.ProcessClientRequest(result.Text, result.Response);
            return Json(response);
        }
    }
}

