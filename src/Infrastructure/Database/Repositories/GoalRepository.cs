using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IGoalRepository))]
    public class GoalRepository : BaseRavenRepository<Goal>, IGoalRepository
    {
        public GoalRepository(IAsyncDocumentSession session) : base(session)
        {
        }
        
        public override async Task SaveAsync(Goal entity)
        {
            await Session.StoreAsync(entity);
        }
        
        public Task SaveAsyncCheckDescription(string description, string roomId)
        {
            throw new NotImplementedException();
        }
        
        public override void Delete(Goal entity)
        {
            throw new NotImplementedException();
        }

        protected override async Task<List<Goal>> ConvertAsync(List<Goal> storage)
        {
            return await Task.FromResult(storage);
        }
    }
}