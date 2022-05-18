// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ucas.PizzaFactory;
using Ucas.PizzaFactory.Interfaces;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

var serviceCollection = new ServiceCollection();

serviceCollection.AddPizzaClasses(config);
serviceCollection.AddUtilityClasses();

var pizzaShop = serviceCollection.BuildServiceProvider().GetRequiredService<PizzaShop>();

await pizzaShop.StartMakingPizzasAsync();

Environment.Exit(0);