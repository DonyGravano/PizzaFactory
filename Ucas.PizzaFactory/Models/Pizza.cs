using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucas.PizzaFactory.Models
{
    public class Pizza
    {
        public Pizza(PizzaBase pizzaBase, string topping, int totalCookingTime)
        {
            Base = pizzaBase;
            Topping = topping;
            TotalCookingTime = totalCookingTime;
        }

        public PizzaBase Base { get; }
        public string Topping { get; }
        public int TotalCookingTime { get; }

        public override string ToString()
        {
            return $"{Base.Type} pizza with {Topping} topping. This took {TotalCookingTime}ms to make";
        }
    }
}
