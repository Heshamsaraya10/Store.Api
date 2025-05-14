using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class CachService(ICachRepository cachRepository) : ICachService
    {
        public async Task<string?> GetAsync(string cachkey)
            => await cachRepository.GetAsync(cachkey);

        public async Task SetAsync(string cachkey, object value, TimeSpan timeToLive)
        {
            var serializedObj = JsonSerializer.Serialize(value);

            await cachRepository.SetAsync(cachkey, serializedObj, timeToLive);
        }
            
    }
} 