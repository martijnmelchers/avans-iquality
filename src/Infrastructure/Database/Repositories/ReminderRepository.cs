using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Raven.Client.Documents;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IReminderRepository))]
    public class ReminderRepository : BaseRavenRepository<Reminder>, IReminderRepository
    {

        public ReminderRepository(IAsyncDocumentSession session) : base(session)
        {

        }

        public override void Delete(Reminder entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reminder>> GetAllWhereAsync(Expression<Func<Reminder, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reminder>> GetByIdsAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reminder>> GetRemindersByUserIdAsync(string userId)
        {
            var result = await Session.Query<Reminder>().Where(i => i.UserId == userId).ToListAsync();
            return result;
        }

        public Task<Reminder> GetWhereAsync(Expression<Func<Reminder, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async override Task SaveAsync(Reminder entity)
        {
            await Session.StoreAsync(entity);
        }

        protected override Task<List<Reminder>> ConvertAsync(List<Reminder> storage)
        {
            throw new NotImplementedException();
        }
    }
}
