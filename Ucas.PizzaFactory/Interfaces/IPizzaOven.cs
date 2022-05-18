using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.Interfaces
{
    public interface IPizzaOven
    {
        public Task CookPizza(Pizza pizza);
    }
}
