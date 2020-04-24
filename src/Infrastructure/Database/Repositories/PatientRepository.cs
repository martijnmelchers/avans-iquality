using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
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

        protected override Task<List<Patient>> ConvertAsync(List<Patient> storage)
        {
            return Task.FromResult(storage.ToList());
        }
    }
}