using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class PizzaCookingTimeCalculatorTests
    {
        private Mock<IPizzaBaseConfiguration> _mockPizzaBaseConfiguration;
        private Mock<IPizzaShopConfiguration> _mockPizzaShopConfiguration;

        private PizzaCookingTimeCalculator Sut => new PizzaCookingTimeCalculator(_mockPizzaShopConfiguration.Object, _mockPizzaBaseConfiguration.Object);

        [SetUp]
        public void SetUp()
        {
            _mockPizzaBaseConfiguration = new Mock<IPizzaBaseConfiguration>();
            _mockPizzaShopConfiguration = new Mock<IPizzaShopConfiguration>();

            var fixture = new Fixture();
            _mockPizzaBaseConfiguration.Setup(pbc => pbc.PizzaBases).Returns(fixture.Create<List<PizzaBase>>());
        }

        // This test checks the constructors parameters all have guard clauses
        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(PizzaCookingTimeCalculator).GetConstructors());
        }

        // This test checks the method parameters all have guard clauses
        [Test]
        public void Methods_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(PizzaCookingTimeCalculator).GetMethods());
        }

        [Test]
        [AutoData]
        public void CalculatePizzaCookingTimeMs_NoValidPizzaBaseConfig_ThrowsInvalidOperationException(List<PizzaBase> pizzaBases, string randomPizzaBase, string randomTopping)
        {
            _mockPizzaBaseConfiguration.Setup(pbc => pbc.PizzaBases).Returns(pizzaBases);

            Action func = () => Sut.CalculatePizzaCookingTimeMs(randomPizzaBase, randomTopping);

            func.Should().Throw<InvalidOperationException>().And.Message.Should().Be($"No configuration values were found for pizza base: {randomPizzaBase}");   
        }

        [Test]
        [AutoData]
        public void CalculatePizzaCookingTimeMs_PizzaBaseCaseInsensitive_ReturnsCorrectTime(List<PizzaBase> pizzaBases, PizzaBase knownPizzaBase)
        {
            var knownTopping = "test";
            const int baseTimeMs = 100;
            pizzaBases.Add(knownPizzaBase);
            _mockPizzaBaseConfiguration.Setup(pbc => pbc.PizzaBases).Returns(pizzaBases);
            _mockPizzaShopConfiguration.Setup(psc => psc.BaseCookingTimeMs).Returns(baseTimeMs);

            knownPizzaBase.Type = knownPizzaBase.Type.ToLower();

            var result = Sut.CalculatePizzaCookingTimeMs(knownPizzaBase.Type, knownTopping);
            result.Should().Be((int)Math.Ceiling(knownPizzaBase.CookingTimeMultiplier * baseTimeMs) + (knownTopping.Length * 100));
        }

        [Test]
        [InlineAutoData("spicy meatballs")]
        [InlineAutoData("spicy meat balls")]
        [InlineAutoData("sp ic y meatb al ls")]
        [InlineAutoData("a")]
        public void CalculatePizzaCookingTimeMs_ToppingContaintsWhiteSpace_ReturnsCorrectTime(string topping, List<PizzaBase> pizzaBases)
        {
            const int baseTimeMs = 100;
            _mockPizzaBaseConfiguration.Setup(pbc => pbc.PizzaBases).Returns(pizzaBases);
            _mockPizzaShopConfiguration.Setup(psc => psc.BaseCookingTimeMs).Returns(baseTimeMs);

            var pizzaBaseType = pizzaBases.First();
            var result = Sut.CalculatePizzaCookingTimeMs(pizzaBaseType.Type, topping);
            result.Should().Be((int)Math.Ceiling(pizzaBaseType.CookingTimeMultiplier * baseTimeMs) + (topping.Replace(" ","").Length * 100));
        }
    }
}
