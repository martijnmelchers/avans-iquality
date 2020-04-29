using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;
using IQuality.Models.Forms;

namespace IQuality.DomainServices.Interfaces
{
    public interface IIntentService
    {
        Task<Bot> HandleIntentClient(string roomId, PatientChat chat, string userText);
    }
}