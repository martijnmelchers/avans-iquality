using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Models.Interfaces;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IBaseRavenRepository<TOut, TStorage>
        where TStorage : IAggregateRoot
    {
        Task<TOut> GetByIdAsync(string id);
        Task<List<TOut>> GetByIdsAsync(IEnumerable<string> ids);
        Task<TOut> GetWhereAsync(Expression<Func<TStorage, bool>> expression);
        Task<List<TOut>> GetAllWhereAsync(Expression<Func<TStorage, bool>> expression);
        Task SaveAsync(TOut entity);
        void DeleteAsync(TOut entity);
    }

    public interface IBaseRavenRepository<T> : IBaseRavenRepository<T, T> where T : IAggregateRoot { }
}
