using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucas.PizzaFactory.Interfaces
{
    public interface IToppingsConfiguration
    {
        public IReadOnlyList<string> Toppings { get; set; }
    }
}
