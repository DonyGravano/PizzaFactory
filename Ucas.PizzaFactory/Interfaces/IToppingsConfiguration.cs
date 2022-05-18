namespace Ucas.PizzaFactory.Interfaces
{
    public interface IToppingsConfiguration
    {
        public IReadOnlyList<string> Toppings { get; set; }
    }
}
