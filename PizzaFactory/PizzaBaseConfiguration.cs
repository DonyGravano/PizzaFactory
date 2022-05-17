using Microsoft.Extensions.Configuration;
using PizzaFactory.Interfaces;
using PizzaFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory
{
    public class PizzaBaseConfiguration : IPizzaBaseConfiguration
    {
        public PizzaBaseConfiguration(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.Bind(this);
        }

        public IReadOnlyList<PizzaBase> PizzaBases { get ; set ; }
    }
}
