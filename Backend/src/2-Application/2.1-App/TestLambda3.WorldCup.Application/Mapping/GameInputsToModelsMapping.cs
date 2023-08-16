using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLambda3.WorldCup.Application.Input;
using TestLambda3.WorldCup.Domain.Models;

namespace TestLambda3.WorldCup.Application.Mapping
{
    public static class GameInputsToModelsMapping
    {
        public static IEnumerable<GameModel> Map(this IEnumerable<GameInput> inputs) 
        {
            List<GameModel> worldCupModels = new List<GameModel>();

            inputs.ToList().ForEach(item =>
            {
                worldCupModels.Add(new GameModel(item.id, null, null, null, null ));
            });

            return worldCupModels;
        }
    }
}
