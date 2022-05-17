using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Models
{
    public class Pizza
    {
        public Pizza(PizzaBase pizzaBase, string topping, decimal totalCookingTime)
        {
            Base = pizzaBase;
            Topping = topping;
            TotalCookingTime = totalCookingTime;
        }

        public PizzaBase Base { get; }
        public string Topping { get; }
        public int TotalCookingTime { get; }
    }
}
