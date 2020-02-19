using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
namespace IQuality.Api.Controllers
{



    class Timeslot
    {
        public DateTime Time { get; set; }
        public string Response { get; set; }
    }


    public class DialogflowController : Controller
    {

        List<Timeslot> timeslots = new List<Timeslot>()
        {
            new Timeslot{Time = DateTime.Parse("29/02/2020 14:00"), Response = "Timeslot 1 registered"},
            new Timeslot{Time = DateTime.Parse("22/02/2020 15:00"), Response = "Timeslot 2 registered"},
        };

        private static readonly JsonParser jsonParser =
            new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        [Route("/dialog")]
        [HttpPost]
        public ContentResult Index()
        {

            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            {
                request = jsonParser.Parse<WebhookRequest>(reader);
            }

            Console.WriteLine(request.QueryResult);



            var requestTime = DateTime.Parse(request.QueryResult.Parameters.Fields["time"].StringValue);
            var requestDate = DateTime.Parse(request.QueryResult.Parameters.Fields["date"].StringValue);

            DateTime totalDate = requestDate.Date + requestTime.TimeOfDay;


            Timeslot matchingTimeslot = null;
            foreach (Timeslot timeslot in timeslots)
            {
                if (timeslot.Time.CompareTo(totalDate) == 0)
                {
                    matchingTimeslot = timeslot;
                }
            }

            string fullfilmenttext = "The given timeslot is not available.";

            if (matchingTimeslot != null)
            {
                fullfilmenttext = matchingTimeslot.Response;
            }

            WebhookResponse response = new WebhookResponse()
            {
                FulfillmentText = fullfilmenttext
            };

            return Content(response.ToString(), "application/json");
        }
    }
}
