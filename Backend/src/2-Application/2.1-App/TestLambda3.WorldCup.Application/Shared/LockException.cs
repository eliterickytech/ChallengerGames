using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLambda3.WorldCup.Application.Shared
{
    public class LockException : Exception
    {
        public LockException(string message) : base(message)
        {
        }
    }
}
