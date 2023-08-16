using System.Diagnostics.Tracing;
using TestLambda3.WorldCup.Application.Contract;
using TestLambda3.WorldCup.Application.Input;
using TestLambda3.WorldCup.Application.Interface;
using TestLambda3.WorldCup.Application.Interface.Service;
using TestLambda3.WorldCup.Application.Mapping;
using TestLambda3.WorldCup.Application.Result;
using TestLambda3.WorldCup.Domain.Interfaces.Repository;
using TestLambda3.WorldCup.Domain.Models;

namespace TestLambda3.WorldCup.Application.App
{
    public class WorldCupApp : IWorldCupApp
    {
        private readonly IWorldCupRepository _worldCupRepository;
        private readonly IBaseNotification _baseNotification;
        public WorldCupApp(IWorldCupRepository worldCupRepository, IBaseNotification baseNotification)
        {
            _worldCupRepository = worldCupRepository;
            _baseNotification = baseNotification;
        }

        public async Task<IEnumerable<GameResult>> GetAllGamesAsync()
        {
            var worldCups = await _worldCupRepository.GetAllGamesAsync();
            return worldCups.Map();
        }

        public async Task<IEnumerable<GameResult>> GetGamesByIdsAsync(List<GameInput> inputs)
        {
            var gamesContract = new GamesContract(inputs);

            if (!gamesContract.IsValid) 
            {
                _baseNotification.AddNotifications(gamesContract.Notifications);
                return default;
            } 

            var gamesModels = await _worldCupRepository.GetAllGamesAsync();

            var map = inputs.Map();

            var filtered = from search in map
                           join game in gamesModels on search.id equals game.id
                           select new GameModel(game.id, game.titulo, game.nota, game.ano, game.urlImagem);

            return filtered.Map() ;
        }

        public async Task<IEnumerable<WorldCupResult>> GenerateWorldCupAsync(List<GameInput> inputs)
        {
            var gamesContract = new GamesContract(inputs);

            if (!gamesContract.IsValid)
            {
                _baseNotification.AddNotifications(gamesContract.Notifications);
                return default;
            }

            var gamesResult = (await GetGamesByIdsAsync(inputs)).ToList().OrderBy(game => game.titulo).ToList();

            List<GameModel> gamesModel = new List<GameModel>();

            gamesResult.ForEach(game => { gamesModel.Add(new GameModel(game.id, game.titulo, game.nota, game.ano, game.urlImagem)); });

            var resultQuarterFinal = Elimination(gamesModel);

            var resultSemiFinal = Elimination(resultQuarterFinal);

            var semiFinal = new List<GameModel>(resultSemiFinal);

            var resultFinal = Elimination(resultSemiFinal);

            List<WorldCupResult> worldCupResults = new List<WorldCupResult>();

            foreach (var item in semiFinal)
            {
                worldCupResults.Add(new WorldCupResult(item.titulo, (resultFinal.FirstOrDefault().id == item.id ? true : false), item.urlImagem));
            }

            return worldCupResults;
        } 

        private List<GameModel> Elimination(List<GameModel> gamesModel)
        {
            List<GameModel> winners = new List<GameModel>();

            int countGamesModel = gamesModel.Count;
            var gamesCloneModel = new List<GameModel>(gamesModel);

            while (gamesModel.Count > 1)
            {
                var winner = GetWinner(gamesModel.FirstOrDefault(), gamesModel.LastOrDefault());

                winners.Add(winner);

                gamesModel.Remove(gamesModel.FirstOrDefault());

                gamesModel.Remove(gamesModel.LastOrDefault());
            }

            if (countGamesModel == 2)
            {
                winners.Add(gamesCloneModel.Where(x => x.id != winners.FirstOrDefault().id).FirstOrDefault()); 
            }
            return winners;
        }

        private GameModel GetWinner(GameModel gameIn, GameModel gameOut)
        {
            if (gameIn.nota > gameOut.nota)
            {
                return gameIn;
            }
            else if (gameIn.nota < gameOut.nota)
            {
                return gameOut;
            }
            else
            {
                if (gameIn.ano < gameOut.ano)
                {
                    return gameIn;
                }
                else if (gameIn.ano > gameOut.ano)
                {
                    return gameOut;
                }
                else
                {
                    return string.Compare(gameIn.titulo, gameOut.titulo, StringComparison.Ordinal) <= 0 ? gameIn : gameOut;
                }
            }
        }
    }
}
