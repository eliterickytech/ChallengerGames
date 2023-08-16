using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLambda3.WorldCup.Application.Result;
using TestLambda3.WorldCup.Domain.Models;

namespace TestLambda3.WorldCup.Application.Mapping
{
    public static class GameModelsToResultsMapping
    {
        public static IEnumerable<GameResult> Map(this IEnumerable<GameModel> models)
        {
            List<GameResult> worldCupResults = new List<GameResult>();

            models.ToList().ForEach(model =>
            {
                worldCupResults.Add(new GameResult(model.id, model.titulo, model.nota, model.ano, model.urlImagem));
            });

            return worldCupResults;
        }
    }
}
