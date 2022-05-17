using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Interfaces
{
    public interface IPizzaCookingTimeCalculator
    {
        public int CalculatePizzaCookingTimeMs(string pizzaBase, string topping);
    }
}
