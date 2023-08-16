using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLambda3.WorldCup.Application.Input;
using TestLambda3.WorldCup.Application.Result;
using TestLambda3.WorldCup.Domain.Models;


namespace TestLambda3.WorldCup.Application.Interface.Service
{
    public interface IWorldCupApp
    {
        Task<IEnumerable<WorldCupResult>> GenerateWorldCupAsync(List<GameInput> inputs);
        Task<IEnumerable<GameResult>> GetAllGamesAsync();
        Task<IEnumerable<GameResult>> GetGamesByIdsAsync(List<GameInput> inputs);
    }
}
