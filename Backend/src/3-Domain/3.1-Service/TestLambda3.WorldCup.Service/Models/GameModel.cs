using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLambda3.WorldCup.Domain.Models
{
    public record GameModel(string id, string? titulo, decimal? nota, int? ano, string? urlImagem);

}
