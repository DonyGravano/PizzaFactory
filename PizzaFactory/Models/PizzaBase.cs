using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Models
{
    public class PizzaBase
    {
        public string Type { get; set; }

        public int CookingTimeMultiplier { get; set; }
    }
}
