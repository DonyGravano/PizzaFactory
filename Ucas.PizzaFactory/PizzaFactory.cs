using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory
{
    public class PizzaFactory : IPizzaFactory
    {
        private readonly IPizzaBaseConfiguration _pizzaBaseConfiguration;
        private readonly IToppingsConfiguration _toppingsConfiguration;
        private readonly IPizzaShopConfiguration _pizzaShopConfiguration;
        private readonly IPizzaCookingTimeCalculator _pizzaCookingTimeCalculator;
        private readonly IDataRepository _dataRepository;

        public PizzaFactory(IPizzaBaseConfiguration pizzaBaseConfiguration,
            IToppingsConfiguration toppingsConfiguration,
            IPizzaShopConfiguration pizzaShopConfiguration,
            IPizzaCookingTimeCalculator pizzaCookingTimeCalculator,
            IDataRepository dataRepository)
        {
            _pizzaBaseConfiguration = pizzaBaseConfiguration ?? throw new ArgumentNullException(nameof(pizzaBaseConfiguration));
            _toppingsConfiguration = toppingsConfiguration ?? throw new ArgumentNullException(nameof(toppingsConfiguration));
            _pizzaShopConfiguration = pizzaShopConfiguration ?? throw new ArgumentNullException(nameof(pizzaShopConfiguration));
            _pizzaCookingTimeCalculator = pizzaCookingTimeCalculator ?? throw new ArgumentNullException(nameof(pizzaCookingTimeCalculator));
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public async Task<IReadOnlyList<Pizza>> CreateRandomPizzasAsync(int numberOfPizzas)
        {
            // Inject me so with a wrapper so we can test this
            var toppingRandomizer = new Random();
            var baseRandomizer = new Random();

            var totalNumberOfToppings = _toppingsConfiguration.Toppings.Count;
            var totalNumberOfPizzaBases = _pizzaBaseConfiguration.PizzaBases.Count;
            var pizzas = new List<Pizza>();

            for (int i = 0; i < numberOfPizzas; i++)
            {
                var pizzaBase = _pizzaBaseConfiguration.PizzaBases[toppingRandomizer.Next(0, totalNumberOfPizzaBases)];
                var topping = _toppingsConfiguration.Toppings[baseRandomizer.Next(0, totalNumberOfToppings)];


                var totalCookingTimeMs = _pizzaCookingTimeCalculator.CalculatePizzaCookingTimeMs(pizzaBase.Type, topping);

                await Task.Delay(totalCookingTimeMs);
                var pizza = new Pizza(pizzaBase, topping, totalCookingTimeMs);
                pizzas.Add(pizza);

                await _dataRepository.StorePizzaAsync(pizza);

                await Task.Delay(_pizzaShopConfiguration.CookingInterval);
            }
            return pizzas;
        }
    }
}
