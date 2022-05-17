using PizzaFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory
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

        public void StartMakingPizzas()
        {
            var pizzas = _pizzaFactory.CreateRandomPizzas(_pizzaShopConfiguration.TotalNumberOfPizzas);
        }
    }
}
