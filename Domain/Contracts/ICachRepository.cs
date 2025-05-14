using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICachRepository
    {
        Task<string?> GetAsync(string cachkey);

        Task SetAsync(string cachkey, string value , TimeSpan timeToLive);
    }
}
