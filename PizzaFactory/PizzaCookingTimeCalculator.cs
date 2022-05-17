using PizzaFactory.Interfaces;

namespace PizzaFactory
{
    public class PizzaCookingTimeCalculator: IPizzaCookingTimeCalculator
    {
        private IPizzaShopConfiguration _pizzaShopConfiguration;
        private IPizzaBaseConfiguration _pizzaBaseConfiguration;

        public PizzaCookingTimeCalculator(IPizzaShopConfiguration pizzaShopConfiguration, IPizzaBaseConfiguration pizzaBaseConfiguration)
        {
            _pizzaShopConfiguration = pizzaShopConfiguration ?? throw new ArgumentNullException(nameof(pizzaShopConfiguration));
            _pizzaBaseConfiguration = pizzaBaseConfiguration ?? throw new ArgumentNullException(nameof(pizzaBaseConfiguration));
        }

        public int CalculatePizzaCookingTimeMs(string pizzaBase, string topping)
        {
            if (string.IsNullOrWhiteSpace(pizzaBase))
            {
                throw new ArgumentNullException(nameof(pizzaBase));
            }

            if (string.IsNullOrWhiteSpace(topping))
            {
                throw new ArgumentNullException(nameof(topping));
            }

            var pizzaBaseSettings = _pizzaBaseConfiguration.PizzaBases.SingleOrDefault(pb => string.Equals(pb.Type, pizzaBase, StringComparison.InvariantCultureIgnoreCase));

            if (pizzaBaseSettings == null)
            {
                throw new InvalidOperationException($"No configuration values were found for pizza base: {pizzaBase}");
            }

            var trimmedTopping = topping.Replace(" ", "");

            return (_pizzaShopConfiguration.BaseCookingTimeMs * pizzaBaseSettings.CookingTimeMultiplier) + (trimmedTopping.Length * 100);
        }
    }
}
