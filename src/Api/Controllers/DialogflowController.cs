using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
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
        private IDialogflowService _dialogflowService;

        public DialogflowController(IDialogflowService dialogflowService)
        {
            _dialogflowService = dialogflowService;
        }
        
        // [HttpPost, Route("bot"), AllowAnonymous]
        // public  IActionResult Set([FromBody] WebhookRequest result)
        // {
        //     _dialogflowService.ProcessRequest(result);
        //     return Ok();
        // }
        
        [HttpPost, Route("patient"), AllowAnonymous]
        public IActionResult Set([FromBody] myModel result)
        {
            QueryResult response = _dialogflowService.BuildResponse(result.Text, result.Response);
            return Json(response);
        }
    }
}

