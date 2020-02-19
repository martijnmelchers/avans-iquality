using System;
using System.Collections.Generic;
using System.IO;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;

namespace IQuality.Api.Controllers
{
    // Timeslot represents a table in our Database.
    class Timeslot
    {
        public DateTime Time { get; set; }
        public string Response { get; set; }
    }


    public class DialogflowController : Controller
    {
        // Dummy data
        List<Timeslot> timeslots = new List<Timeslot>()
        {
            new Timeslot{Time = DateTime.Parse("29/02/2020 14:00"), Response = "Timeslot 1 registered"},
            new Timeslot{Time = DateTime.Parse("22/02/2020 15:00"), Response = "Timeslot 2 registered"},
        };
        
        // JsonParser of Google API is used for DialogFlow.
        private static readonly JsonParser jsonParser =
            new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        // Used for creating an appointment with the doctor.
        [Route("/dialog")]
        [HttpPost]
        public ContentResult Index()
        {
            // Data from DialogFlow
            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            {
                request = jsonParser.Parse<WebhookRequest>(reader);
            }

            Console.WriteLine(request.QueryResult);


            // Fields from the body data is Parsed to DateTime.
            var requestTime = DateTime.Parse(request.QueryResult.Parameters.Fields["time"].StringValue);
            var requestDate = DateTime.Parse(request.QueryResult.Parameters.Fields["date"].StringValue);

            DateTime totalDate = requestDate.Date + requestTime.TimeOfDay;

            // Check if the timeslot is not taken.
            Timeslot matchingTimeslot = null;
            foreach (Timeslot timeslot in timeslots)
            {
                if (timeslot.Time.CompareTo(totalDate) == 0)
                {
                    matchingTimeslot = timeslot;
                }
            }

            string fullfilmenttext = "The given timeslot is not available.";
            
            // Timeslot is available for appointment.
            if (matchingTimeslot != null)
            {
                fullfilmenttext = matchingTimeslot.Response;
            }

            // Response
            WebhookResponse response = new WebhookResponse()
            {
                FulfillmentText = fullfilmenttext
            };

            return Content(response.ToString(), "application/json");
        }
    }
}
