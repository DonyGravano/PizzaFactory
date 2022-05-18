using Microsoft.Extensions.Configuration;
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
