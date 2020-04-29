using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IResponseBuilderService
    {
        Task<QueryResult> BuildTextResponse(string text, string roomId, string context);
    }
}