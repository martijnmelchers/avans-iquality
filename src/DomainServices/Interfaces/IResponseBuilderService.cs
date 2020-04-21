using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IResponseBuilderService
    {
        QueryResult BuildTextResponse(string text);
        QueryResult BuildContextResponse(QueryResult result, string text);
    }
}