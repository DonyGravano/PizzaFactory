using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory
{
    public class PizzaOven: IPizzaOven
    {
        private readonly IDelayWrapper _delayWrapper;

        public PizzaOven(IDelayWrapper delayWrapper)
        {
            _delayWrapper = delayWrapper ?? throw new ArgumentNullException(nameof(delayWrapper));
        }

        public async Task CookPizza(Pizza pizza)
        {
            if (pizza is null)
            {
                throw new ArgumentNullException(nameof(pizza));
            }

            if (pizza.Cooked == true)
            {
                throw new InvalidOperationException("Cannot cook an already cooked pizza");
            }

            await _delayWrapper.Delay(pizza.TotalCookingTime);
            pizza.Cooked = true;
        }
    }
}
