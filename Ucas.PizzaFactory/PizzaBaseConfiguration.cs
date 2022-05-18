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

            if (PizzaBases == null || !PizzaBases.Any())
            {
                throw new InvalidOperationException("The pizza bases list in the configuration was empty or missing");
            }
        }

        public IReadOnlyList<PizzaBase> PizzaBases { get; set; }
    }
}
