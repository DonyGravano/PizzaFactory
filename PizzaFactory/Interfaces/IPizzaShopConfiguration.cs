using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Interfaces
{
    public interface IPizzaShopConfiguration
    {   
        public int BaseCookingTimeMs { get; set; }
        public int TotalNumberOfPizzas { get; set; }
        public int CookingInterval { get; set; }
        public string PizzaFileLocation { get; set; }
    }
}
