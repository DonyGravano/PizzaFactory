using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class RandomWrapper: IRandomWrapper
    {
        private readonly Random random;

        public RandomWrapper()
        {
            random = new Random();
        }

        public int GetNextRandom(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
