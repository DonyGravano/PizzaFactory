using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class PizzaShop
    {
        private readonly IPizzaFactory _pizzaFactory;
        private readonly IDataRepository _dataRepository;
        private readonly IPizzaShopConfiguration _pizzaShopConfiguration;

        public PizzaShop(IPizzaShopConfiguration pizzaShopConfiguration, IPizzaFactory pizzaFactory, IDataRepository dataRepository)
        {
            _pizzaShopConfiguration = pizzaShopConfiguration ?? throw new ArgumentNullException(nameof(pizzaShopConfiguration));
            _pizzaFactory = pizzaFactory ?? throw new ArgumentNullException(nameof(pizzaFactory));
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public async Task StartMakingPizzasAsync()
        {
            var pizzas = await _pizzaFactory.CreateRandomPizzasAsync(_pizzaShopConfiguration.TotalNumberOfPizzas);
        }
    }
}
