using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Goals;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IPatientDataIntentHandler : IIntentHandler
    {
        Task SaveWeight(string weight, PatientChat chat);
    }
}