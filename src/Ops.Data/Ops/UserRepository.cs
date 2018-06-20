﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ocuda.Ops.Data.Ops
{
    public class UserRepository : GenericRepository<Models.User, int>
    {
        public UserRepository(OpsContext context, ILogger<UserRepository> logger)
            : base(context, logger)
        {
        }

        #region Initial setup methods
        // this cannot be async becuase Configure() in Startup.cs is not async
        public Models.User GetSystemAdministrator()
        {
            return DbSet
                .AsNoTracking()
                .Where(_ => _.IsSysadmin == true)
                .FirstOrDefault();
        }
        #endregion Initial setup methods
    }
}