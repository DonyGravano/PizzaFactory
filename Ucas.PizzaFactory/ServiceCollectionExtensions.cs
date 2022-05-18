using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPizzaClasses(this ServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IPizzaBaseConfiguration>(_ => new PizzaBaseConfiguration(configuration));
            serviceCollection.AddSingleton<IToppingsConfiguration>(_ => new ToppingsConfiguration(configuration));
            serviceCollection.AddSingleton<IPizzaShopConfiguration>(_ => new PizzaShopConfiguration(configuration));
            serviceCollection.AddTransient<IPizzaCookingTimeCalculator, PizzaCookingTimeCalculator>();
            serviceCollection.AddTransient<IPizzaOven, PizzaOven>();
            serviceCollection.AddSingleton<PizzaShop>();
            serviceCollection.AddTransient<IPizzaFactory, PizzaFactory>();
        }

        public static void AddUtilityClasses(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDataRepository, FileRepository>();
            serviceCollection.AddTransient<IRandomWrapperBuilder, RandomWrapperBuilder>();
            serviceCollection.AddTransient<IDelayWrapper, DelayWrapper>();
        }
    }
}
