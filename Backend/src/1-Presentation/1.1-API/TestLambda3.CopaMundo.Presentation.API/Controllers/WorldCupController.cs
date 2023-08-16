using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TestLambda3.WorldCup.Application.Input;
using TestLambda3.WorldCup.Application.Interface;
using TestLambda3.WorldCup.Application.Interface.Service;
using TestLambda3.WorldCup.Application.Shared;

namespace TestLambda3.WorldCup.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldCupController : BaseController
    {
        private readonly IWorldCupApp _worldCupApp;
        public WorldCupController(IWorldCupApp worldCupApp, IBaseNotification baseNotification) : base(baseNotification)
        {
            _worldCupApp = worldCupApp;

        }
        /// <summary>
        /// Return all Games
        /// </summary>
        /// <returns>List<GameResult></returns>
        [HttpGet]        
        [Route("GamesAll")]
        public async Task<IActionResult> GetAll()
        {
            var worldCups = await _worldCupApp.GetAllGamesAsync();

            return OKOrBadRequest(worldCups);
        }

        /// <summary>
        /// Return the Games in search
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>List<GameResult></returns>
        [HttpPost]
        [Route("SearchGames")]
        public async Task<IActionResult> GetByIds([FromBody] List<GameInput> inputs)
        {
            var worldCups = await _worldCupApp.GetGamesByIdsAsync(inputs);
            
            return OKOrBadRequest(worldCups);
        }
        /// <summary>
        /// Generate World Cup
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>List<GameResult></returns>
        [HttpPost]
        [Route("GenerateWorldCup")]
        public async Task<IActionResult> GenerateWorldCup([FromBody] List<GameInput> inputs)
        {
            var winners = await _worldCupApp.GenerateWorldCupAsync(inputs);

            return CreatedOrBadRequest(winners);
        }
    }
}   
