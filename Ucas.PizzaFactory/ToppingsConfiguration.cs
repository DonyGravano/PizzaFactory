using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class ToppingsConfiguration : IToppingsConfiguration
    {
        public ToppingsConfiguration(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.Bind(this);
        }

        public IReadOnlyList<string> Toppings { get; set; }
    }
}
