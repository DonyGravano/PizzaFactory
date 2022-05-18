namespace Ucas.PizzaFactory.Interfaces
{
    public interface IPizzaShopConfiguration
    {
        public int BaseCookingTimeMs { get; set; }
        public int TotalNumberOfPizzas { get; set; }
        public int CookingInterval { get; set; }
        public string PizzaFileLocation { get; set; }
    }
}
