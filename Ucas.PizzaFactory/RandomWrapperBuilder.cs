using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class RandomWrapperBuilder : IRandomWrapperBuilder
    {
        public IRandomWrapper GetNewRandom()
        {
            return new RandomWrapper();
        }
    }
}
