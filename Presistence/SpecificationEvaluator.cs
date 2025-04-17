using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> baseQuert , Specification<T> specifications) where T : class
        {
            var query = baseQuert;

            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            query = specifications.Includes.Aggregate(query , (currentQuery , include) => currentQuery.Include(include));
            return query;

        }
    }
}
