using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.Interfaces
{
    public interface IDataRepository
    {
        public Task StorePizzaAsync(Pizza pizza);
    }
}
