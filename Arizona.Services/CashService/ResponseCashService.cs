using Arizona.Core.Services.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Arizona.Application.CashService
{
    public class ResponseCashService : IResponseCashService
    {
        private readonly IDatabase _database;

        public ResponseCashService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CashResponseAsync(string key, object Response, TimeSpan timeToLife)
        {
            if (Response is null) return;

            var serializedOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponse = JsonSerializer.Serialize(Response , serializedOptions);

             

            await _database.StringSetAsync(key, serializedResponse , timeToLife);
        }

        public async Task<string?> GetCashedResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);

            if (response.IsNullOrEmpty) return null; 

            return response;
            

        }
    }
}
