using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class PizzaFactoryTests
    {
        private Mock<IPizzaBaseConfiguration> _pizzaBaseConfiguration;
        private Mock<IToppingsConfiguration> _toppingsConfiguration;
        private Mock<IPizzaShopConfiguration> _pizzaShopConfiguration;
        private Mock<IPizzaCookingTimeCalculator> _pizzaCookingTimeCalculator;
        private Mock<IDataRepository> _dataRepository;
        private Mock<IRandomWrapperBuilder> _randomWrapperBuilder;
        
        private PizzaFactory _pizzaFactory => new PizzaFactory(_pizzaBaseConfiguration.Object, _toppingsConfiguration.Object, _pizzaShopConfiguration.Object, _pizzaCookingTimeCalculator.Object, _dataRepository.Object, _randomWrapperBuilder.Object);

        [SetUp]
        public void SetUp()
        {
            _dataRepository = new Mock<IDataRepository>();
            _toppingsConfiguration = new Mock<IToppingsConfiguration>();
            _pizzaBaseConfiguration = new Mock<IPizzaBaseConfiguration>();
            _randomWrapperBuilder = new Mock<IRandomWrapperBuilder>();
            _pizzaShopConfiguration = new Mock<IPizzaShopConfiguration>();
            _pizzaCookingTimeCalculator = new Mock<IPizzaCookingTimeCalculator>();
        }

        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(PizzaFactory).GetConstructors());
        }

        [Test]
        [AutoData]
        public async Task Test(int noOfPizzas)
        {
            var pizzas = await _pizzaFactory.CreateRandomPizzasAsync(noOfPizzas);

            pizzas.Should().HaveCount(noOfPizzas);
        }
    }
}
