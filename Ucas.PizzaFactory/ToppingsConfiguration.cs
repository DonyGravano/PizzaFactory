using Microsoft.Extensions.Configuration;
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

            if (Toppings == null || !Toppings.Any())
            {
                throw new InvalidOperationException("The toppings list in the configuration was empty or missing");
            }
        }

        public IReadOnlyList<string> Toppings { get; set; }
    }
}
