using TestLambda3.WorldCup.Domain.Models;

namespace TestLambda3.WorldCup.Domain.Interfaces.Repository
{
    public interface IWorldCupRepository
    {
        Task<IEnumerable<GameModel>> GetAllGamesAsync();
    }
}
