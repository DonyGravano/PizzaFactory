using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory
{
    public class FileRepository : IDataRepository
    {
        private readonly IPizzaShopConfiguration _pizzaShopConfiguration;

        public FileRepository(IPizzaShopConfiguration pizzaShopConfiguration)
        {
            _pizzaShopConfiguration = pizzaShopConfiguration ?? throw new ArgumentNullException(nameof(pizzaShopConfiguration));
        }

        public async Task StorePizzaAsync(Pizza pizza)
        {
            if (pizza is null)
            {
                throw new ArgumentNullException(nameof(pizza));
            }

            using StreamWriter file = new(_pizzaShopConfiguration.PizzaFileLocation, append: true);
            await file.WriteLineAsync($"Finished making a {pizza}");
            Console.WriteLine($"Finished making a {pizza}");
        }
    }
}
