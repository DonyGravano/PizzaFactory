// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ucas.PizzaFactory;
using Ucas.PizzaFactory.Interfaces;

Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IPizzaBaseConfiguration>(_ => new PizzaBaseConfiguration(config));
serviceCollection.AddSingleton<IToppingsConfiguration>(_ => new ToppingsConfiguration(config));
serviceCollection.AddSingleton<IPizzaShopConfiguration>(_ => new PizzaShopConfiguration(config));
serviceCollection.AddTransient<IPizzaCookingTimeCalculator, PizzaCookingTimeCalculator>();
serviceCollection.AddTransient<IDataRepository, FileRepository>();
serviceCollection.AddSingleton<IPizzaFactory, PizzaFactory>();
serviceCollection.AddSingleton<PizzaShop>();

var pizzaShop = serviceCollection.BuildServiceProvider().GetRequiredService<PizzaShop>();

await pizzaShop.StartMakingPizzasAsync();