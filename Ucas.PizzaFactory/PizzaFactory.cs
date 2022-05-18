﻿using Ucas.PizzaFactory.Interfaces;
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
        private readonly IRandomWrapperBuilder _randomWrapperBuilder;
        private readonly IDelayWrapper _delayWrapper;
        private readonly IPizzaOven _pizzaOven;

        public PizzaFactory(IPizzaBaseConfiguration pizzaBaseConfiguration,
            IToppingsConfiguration toppingsConfiguration,
            IPizzaShopConfiguration pizzaShopConfiguration,
            IPizzaCookingTimeCalculator pizzaCookingTimeCalculator,
            IDataRepository dataRepository,
            IRandomWrapperBuilder randomWrapperBuilder,
            IDelayWrapper delayWrapper,
            IPizzaOven pizzaOven)
        {
            _pizzaBaseConfiguration = pizzaBaseConfiguration ?? throw new ArgumentNullException(nameof(pizzaBaseConfiguration));
            _toppingsConfiguration = toppingsConfiguration ?? throw new ArgumentNullException(nameof(toppingsConfiguration));
            _pizzaShopConfiguration = pizzaShopConfiguration ?? throw new ArgumentNullException(nameof(pizzaShopConfiguration));
            _pizzaCookingTimeCalculator = pizzaCookingTimeCalculator ?? throw new ArgumentNullException(nameof(pizzaCookingTimeCalculator));
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            _randomWrapperBuilder = randomWrapperBuilder ?? throw new ArgumentNullException(nameof(randomWrapperBuilder));
            _delayWrapper = delayWrapper ?? throw new ArgumentNullException(nameof(delayWrapper));
            _pizzaOven = pizzaOven ?? throw new ArgumentNullException(nameof(pizzaOven));
        }

        public async Task CreateRandomPizzasAsync(int numberOfPizzas)
        {
            if (numberOfPizzas == 0)
            {
                return;
            }

            var toppingRandomizer = _randomWrapperBuilder.GetNewRandom();
            var baseRandomizer = _randomWrapperBuilder.GetNewRandom();

            var totalNumberOfToppings = _toppingsConfiguration.Toppings.Count;
            var totalNumberOfPizzaBases = _pizzaBaseConfiguration.PizzaBases.Count;

            for (int i = 0; i < numberOfPizzas; i++)
            {
                var pizzaBase = _pizzaBaseConfiguration.PizzaBases[toppingRandomizer.GetNextRandom(0, totalNumberOfPizzaBases)];
                var topping = _toppingsConfiguration.Toppings[baseRandomizer.GetNextRandom(0, totalNumberOfToppings)];

                var totalCookingTimeMs = _pizzaCookingTimeCalculator.CalculatePizzaCookingTimeMs(pizzaBase.Type, topping);

                var pizza = new Pizza(pizzaBase, topping, totalCookingTimeMs);

                await _pizzaOven.CookPizza(pizza);

                await _dataRepository.StorePizzaAsync(pizza);

                await _delayWrapper.Delay(_pizzaShopConfiguration.CookingInterval);
            }
        }
    }
}
