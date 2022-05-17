using PizzaFactory.Models;
namespace PizzaFactory.Interfaces
{
    public interface IPizzaFactory
    {
        public IReadOnlyList<Pizza> CreateRandomPizzas(int numberOfPizzas);
    }
}
