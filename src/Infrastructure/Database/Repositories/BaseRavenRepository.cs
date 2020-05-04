using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Interfaces;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
namespace IQuality.Infrastructure.Database.Repositories

{
    public abstract class BaseRavenRepository<TOut, TStorage> : IBaseRavenRepository<TOut, TStorage>
        where TStorage : IAggregateRoot
    {
        protected readonly IAsyncDocumentSession Session;

        protected BaseRavenRepository(IAsyncDocumentSession session)
        {
            Session = session;
        }

        public async Task<TOut> GetByIdAsync(string id)
        {
            return (await ConvertAsync(new List<TStorage> { await Session.LoadAsync<TStorage>(id) }))[0];
        }

        public async Task<List<TOut>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await ConvertAsync((await Session.LoadAsync<TStorage>(ids)).Values.ToList());
        }

        public async Task<TOut> GetWhereAsync(Expression<Func<TStorage, bool>> expression)
        {
            var storage = await Session.Query<TStorage>()
                .Customize(query => query.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                .FirstOrDefaultAsync(expression);
            return storage != null ? (await ConvertAsync(new List<TStorage> { storage }))[0] : default;
        }

        public async Task<List<TOut>> GetAllWhereAsync(Expression<Func<TStorage, bool>> expression)
        {
            return await ConvertAsync(await Session.Stream(Session.Query<TStorage>().Where(expression)));
        }

        public abstract Task SaveAsync(TOut entity);
        public abstract void Delete(TOut entity);
        protected abstract Task<List<TOut>> ConvertAsync(List<TStorage> storage);
    }

    public abstract class BaseRavenRepository<TOut> : BaseRavenRepository<TOut, TOut> where TOut : IAggregateRoot
    {
        protected BaseRavenRepository(IAsyncDocumentSession session) : base(session)
        {
        }
    }
}
