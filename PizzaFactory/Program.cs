// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaFactory;
using PizzaFactory.Interfaces;
using System.Reflection;

Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
                .SetBasePath(Assembly.GetExecutingAssembly().Location)
                .AddJsonFile("appsettings.json").Build();

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IPizzaBaseConfiguration>(_ => new PizzaBaseConfiguration(config));
serviceCollection.AddSingleton<IToppingsConfiguration>(_ => new ToppingsConfiguration(config));
serviceCollection.AddSingleton<IPizzaShopConfiguration>(_ => new PizzaShopConfiguration(config));

