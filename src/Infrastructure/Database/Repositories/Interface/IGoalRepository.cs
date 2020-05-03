using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Models.Goals;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IGoalRepository : IBaseRavenRepository<Goal>
    {
        Task SaveAsyncCheckDescription(string description, string roomId);
        Task<Goal> GetWhereDescription(string description);
    }
}