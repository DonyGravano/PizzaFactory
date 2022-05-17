using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory
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

        public IReadOnlyList<PizzaBase> PizzaBases { get; set; }
    }
}
