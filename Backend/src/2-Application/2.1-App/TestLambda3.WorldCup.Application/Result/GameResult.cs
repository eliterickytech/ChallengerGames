using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLambda3.WorldCup.Application.Result
{
    public record GameResult(string id, string? titulo, decimal? nota, int? ano, string? urlImagem);
}
