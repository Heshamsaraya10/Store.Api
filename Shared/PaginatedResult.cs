using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PaginatedResult<TData>(int pageInex , int pageSize , int totalCount , IEnumerable<TData> data)
    {

    }
}
