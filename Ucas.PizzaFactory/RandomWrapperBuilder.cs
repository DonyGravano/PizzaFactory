using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class RandomWrapperBuilder: IRandomWrapperBuilder
    {
        public RandomWrapper GetNewRandom()
        {
            return new RandomWrapper();
        }
    }
}
