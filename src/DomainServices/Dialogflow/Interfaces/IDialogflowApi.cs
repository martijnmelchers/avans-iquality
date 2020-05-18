using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Dialogflow.Interfaces
{
    public interface IDialogflowApi
    {
        Task<QueryResult> DetectClientIntent(string text, string context);
    }
}