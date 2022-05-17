using PizzaFactory.Interfaces;
using PizzaFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory
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
            using StreamWriter file = new(_pizzaShopConfiguration.PizzaFileLocation, append: true);
            await file.WriteLineAsync("Fourth line");
        }
    }
}
