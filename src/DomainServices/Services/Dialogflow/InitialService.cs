using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IInitialService))]
    public class InitialService: IIntentService, IInitialService
    {
        private readonly IGoalRepository _goalRepository;

        public InitialService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }
        
        public Task<Bot> HandleIntentClient(string roomId, PatientChat chat, string userText)
        {
            throw new System.NotImplementedException();
        }
    }
}