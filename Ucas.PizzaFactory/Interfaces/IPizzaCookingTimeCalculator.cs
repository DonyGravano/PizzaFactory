namespace Ucas.PizzaFactory.Interfaces
{
    public interface IPizzaCookingTimeCalculator
    {
        public int CalculatePizzaCookingTimeMs(string pizzaBase, string topping);
    }
}
