using Microsoft.Extensions.Configuration;
using PizzaFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory
{
    public class ToppingsConfiguration: IToppingsConfiguration
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
