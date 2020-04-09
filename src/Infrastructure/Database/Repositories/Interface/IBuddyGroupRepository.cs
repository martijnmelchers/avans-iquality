﻿using IQuality.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IBuddyGroupRepository : IBaseRavenRepository<Buddy>
    {
        Task<List<string>> GetAll();
        Task Delete(int id);
    }
}
