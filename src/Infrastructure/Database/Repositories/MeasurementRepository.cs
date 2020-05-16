using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Helpers;
using IQuality.Models.Measurements;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IMeasurementRepository))]
    public class MeasurementRepository : BaseRavenRepository<Measurement>, IMeasurementRepository
    {
        public MeasurementRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        protected override Task<List<Measurement>> ConvertAsync(List<Measurement> storage)
        {
            return Task.FromResult(storage);
        }
    }
}