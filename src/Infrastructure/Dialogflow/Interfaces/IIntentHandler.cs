using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;
using IQuality.Models.Forms;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IIntentHandler
    {
        Task<Bot> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult = null);
    }
}