using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class CachRepository(IConnectionMultiplexer connection) : ICachRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string cachkey)
            => await _database.StringGetAsync(cachkey);

        public async Task SetAsync(string cachkey, string value, TimeSpan timeToLive)
            => await _database.StringSetAsync(cachkey, value, timeToLive);
    }
}
