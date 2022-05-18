using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.Interfaces
{
    public interface IPizzaBaseConfiguration
    {
        public IReadOnlyList<PizzaBase> PizzaBases { get; set; }
    }
}
