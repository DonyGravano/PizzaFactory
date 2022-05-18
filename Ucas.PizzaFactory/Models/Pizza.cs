namespace Ucas.PizzaFactory.Models
{
    public class Pizza
    {
        public Pizza(PizzaBase pizzaBase, string topping, int totalCookingTime)
        {
            Base = pizzaBase;
            Topping = topping;
            TotalCookingTime = totalCookingTime;
            Cooked = false;
        }

        public PizzaBase Base { get; }
        public bool Cooked { get; set; }
        public string Topping { get; }
        public int TotalCookingTime { get; }

        public override string ToString()
        {
            return $"{Base.Type} pizza with {Topping} topping. This took {TotalCookingTime}ms to make";
        }
    }
}
