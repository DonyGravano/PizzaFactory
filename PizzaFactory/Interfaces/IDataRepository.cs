using PizzaFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Interfaces
{
    public interface IDataRepository
    {
        public Task StorePizzaAsync(Pizza pizza);
    }
}
