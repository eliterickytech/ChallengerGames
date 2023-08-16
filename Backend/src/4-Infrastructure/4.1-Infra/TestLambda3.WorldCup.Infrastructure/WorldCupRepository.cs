using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TestLambda3.WorldCup.Domain.Interfaces.Repository;
using TestLambda3.WorldCup.Domain.Models;

namespace TestLambda3.WorldCup.Infrastructure
{
    public class WorldCupRepository : IWorldCupRepository
    {
        private readonly IConfiguration _configuration;

        public WorldCupRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  async Task<IEnumerable<GameModel>> GetAllGamesAsync()
        {
            var baseEndpoint = _configuration.GetSection("WorldCup:BaseUrl").Value;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseEndpoint);

                var parameters = "?copa=games";
                 
                var result = await client.GetAsync(parameters);

                if (!result.IsSuccessStatusCode) throw new Exception();
                 
                var content = await result.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<IEnumerable<GameModel>>(content);                
            }
        }
    }
} 