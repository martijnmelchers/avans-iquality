﻿using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IResponseBuilderService
    {
        Task<QueryResult> BuildTextResponse(string text, string context);
    }
}