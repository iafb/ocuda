﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ocuda.Ops.Data.Extensions;
using Ocuda.Ops.Models;
using Ocuda.Ops.Service.Filters;
using Ocuda.Ops.Service.Interfaces.Ops;
using Ocuda.Ops.Service.Models;

namespace Ocuda.Ops.Data.Ops
{
    public class CategoryRepository : GenericRepository<Models.Category, int>, ICategoryRepository
    {
        public CategoryRepository(OpsContext context, ILogger<CategoryRepository> logger)
            :base(context, logger)
        {

        }

        public async Task<ICollection<Category>> GetBySectionIdAsync(BlogFilter filter)
        {
            var query = DbSet.AsNoTracking();

            if (filter.CategoryType.HasValue)
            {
                query = query.Where(_ => _.CategoryType == filter.CategoryType);
            }

            if (filter.SectionId.HasValue)
            {
                query = query.Where(_ => _.SectionId == filter.SectionId);
            }

            return await query.ToListAsync();
        }

        public async Task<DataWithCount<ICollection<Category>>> GetPaginatedListAsync(BlogFilter filter)
        {
            var query = DbSet.AsNoTracking();

            if (filter.CategoryType.HasValue)
            {
                query = query.Where(_ => _.CategoryType == filter.CategoryType);
            }

            if (filter.SectionId.HasValue)
            {
                query = query.Where(_ => _.SectionId == filter.SectionId);
            }

            return new DataWithCount<ICollection<Category>>
            {
                Count = await query.CountAsync(),
                Data = await query
                    .OrderBy(_ => _.Name)
                    .ApplyPagination(filter)
                    .ToListAsync()
            };
        }
    }
}