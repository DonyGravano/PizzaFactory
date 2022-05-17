﻿using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.Interfaces
{
    public interface IPizzaFactory
    {
        public Task CreateRandomPizzasAsync(int numberOfPizzas);
    }
}
