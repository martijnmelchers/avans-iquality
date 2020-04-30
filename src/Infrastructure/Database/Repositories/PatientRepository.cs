using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Authentication.Settings;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IPatientRepository))]
    public class PatientRepository : BaseRavenRepository<Patient>, IPatientRepository
    {
        public PatientRepository(IAsyncDocumentSession session) : base(session)
        {
            
        }

        public async override Task SaveAsync(Patient entity)
        {
            await Session.StoreAsync(entity);
        }

        public override void Delete(Patient entity)
        {
            Session.Delete(entity);
        }

        public async Task<Patient> GetPatientAsync(string id)
        {
            return await Session.LoadAsync<Patient>(id);
        }

        protected override Task<List<Patient>> ConvertAsync(List<Patient> storage)
        {
            return Task.FromResult(storage.ToList());
        }

        public async Task<Patient> SetPatientSettingsAsync(PatientSettings settings,string patientID)
        {
            var result = await Session.LoadAsync<Patient>(patientID);
            result.Settings = settings;
            await Session.StoreAsync(result);
            return result;
        }
    }
}