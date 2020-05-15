using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Helpers;
using IQuality.Models.PatientData;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IPatientDataRepository))]
    public class PatientDataRepository: BaseRavenRepository<PatientData>, IPatientDataRepository
    {
        public PatientDataRepository(IAsyncDocumentSession session) : base(session)
        {
        }
        public Task<PatientData> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientData>> GetByIdsAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<PatientData> GetWhereAsync(Expression<Func<PatientData, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientData>> GetAllWhereAsync(Expression<Func<PatientData, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public override async Task SaveAsync(PatientData entity)
        {
            await Session.StoreAsync(entity);
        }

        public override void Delete(PatientData entity)
        {
            throw new NotImplementedException();
        }

        protected override Task<List<PatientData>> ConvertAsync(List<PatientData> storage)
        {
            throw new NotImplementedException();
        }
    }
}