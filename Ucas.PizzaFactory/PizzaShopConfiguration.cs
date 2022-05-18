using Microsoft.Extensions.Configuration;
using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public class PizzaShopConfiguration : IPizzaShopConfiguration
    {
        public PizzaShopConfiguration(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.Bind(this);
        }

        public int BaseCookingTimeMs { get; set; }
        public int TotalNumberOfPizzas { get; set; }
        public int CookingInterval { get; set; }
        public string PizzaFileLocation { get; set; }
    }
}
