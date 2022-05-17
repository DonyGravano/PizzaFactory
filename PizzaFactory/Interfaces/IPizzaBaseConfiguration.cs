using PizzaFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Interfaces
{
    public interface IPizzaBaseConfiguration
    {
        public IReadOnlyList<PizzaBase> PizzaBases { get; set; }
    }
}
