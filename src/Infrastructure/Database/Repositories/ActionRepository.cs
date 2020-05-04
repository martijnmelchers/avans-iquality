using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IActionRepository))]
    public class ActionRepository : BaseRavenRepository<Action>, IActionRepository
    {
        public ActionRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        protected override Task<List<Action>> ConvertAsync(List<Action> storage)
        {
            return Task.FromResult(storage);
        }
    }
}