using TestLambda3.WorldCup.Application.Input;
using TestLambda3.WorldCup.Application.Shared;

namespace TestLambda3.WorldCup.Application.Contract
{
    public class GamesContract : BaseContract<List<GameInput>>
    {
        public GamesContract(List<GameInput> inputs) 
        {
            Validate(inputs);
        }

        protected override void Validate(List<GameInput> inputs)
        {
            foreach (var input in inputs)
            {
                AddNotifications(new Flunt.Br.Contract()
                    .Requires()
                    .IsNotNullOrEmpty(input.id, "id", "The Id field cannot be empty or null")
                    .IsGreaterOrEqualsThan(inputs.Count(), 8, "The number of games must be greater than or equal to 8"));
            }
        }
    }
}
