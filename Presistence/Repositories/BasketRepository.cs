using Domain.Contracts;
using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
         => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            if (basket.IsNullOrEmpty)
                return null;
            
            return JsonSerializer.Deserialize<CustomerBasket>(basket);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var SerializedBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, SerializedBasket , timeToLive ?? TimeSpan.FromDays(30));

            return isCreatedOrUpdated? await GetBasketAsync(basket.Id): null;

        }
    }
}
