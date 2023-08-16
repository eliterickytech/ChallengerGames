using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLambda3.WorldCup.Application.Input;
using TestLambda3.WorldCup.Application.Result;
using TestLambda3.WorldCup.Domain.Models;

namespace TestLambda3.WorldCup.Application.Mapping
{
    public static class GameModelToResult
    {
        public static GameResult Map(this GameModel model)
        {
            return new GameResult(model.id, model.titulo, model.nota, model.ano, model.urlImagem);
        }
    }
}
