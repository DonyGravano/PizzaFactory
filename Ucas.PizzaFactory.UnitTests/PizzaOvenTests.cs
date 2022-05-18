using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class PizzaOvenTests
    {
        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(PizzaOven).GetConstructors());
        }

        [Test]
        public async Task CookPizza_NullArgument_ThrowsNullArgumentException()
        {
            var pizzaOven = new PizzaOven(new Mock<IDelayWrapper>().Object);

            Func<Task> sut = async () => await pizzaOven.CookPizza(null);

            await sut.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        [AutoData]
        public async Task CookPizza_PizzaAlreadyCooked_ThrowsInvalidOperationException(Pizza pizza)
        {
            var pizzaOven = new PizzaOven(new Mock<IDelayWrapper>().Object);
            pizza.Cooked = true;
            Func<Task> sut = async () => await pizzaOven.CookPizza(pizza);

            await sut.Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        [AutoData]
        public async Task CookPizza_ValidPizza_CallsDelayWrapper(Pizza pizza)
        {
            pizza.Cooked = false;
            var mockDelayWrapper = new Mock<IDelayWrapper>();           
            var pizzaOven = new PizzaOven(mockDelayWrapper.Object);
            await pizzaOven.CookPizza(pizza);

            mockDelayWrapper.Verify(dw => dw.Delay(pizza.TotalCookingTime), Times.Once);
        }
    }
}
