using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class PizzaFactoryTests
    {
        private Mock<IPizzaBaseConfiguration> _mockPizzaBaseConfiguration;
        private Mock<IToppingsConfiguration> _mockToppingsConfiguration;
        private Mock<IPizzaShopConfiguration> _mockPizzaShopConfiguration;
        private Mock<IPizzaCookingTimeCalculator> _mockPizzaCookingTimeCalculator;
        private Mock<IDataRepository> _mockDataRepository;
        private Mock<IRandomWrapperBuilder> _mockRandomWrapperBuilder;
        private Mock<IDelayWrapper> _mockDelayWrapper;
        private Mock<IPizzaOven> _mockPizzaOven;

        private PizzaFactory Sut => new PizzaFactory(_mockPizzaBaseConfiguration.Object,
            _mockToppingsConfiguration.Object,
            _mockPizzaShopConfiguration.Object,
            _mockPizzaCookingTimeCalculator.Object,
            _mockDataRepository.Object,
            _mockRandomWrapperBuilder.Object,
            _mockDelayWrapper.Object,
            _mockPizzaOven.Object);

        [SetUp]
        public void SetUp()
        {
            _mockDataRepository = new Mock<IDataRepository>();
            _mockToppingsConfiguration = new Mock<IToppingsConfiguration>();
            _mockPizzaBaseConfiguration = new Mock<IPizzaBaseConfiguration>();
            _mockRandomWrapperBuilder = new Mock<IRandomWrapperBuilder>();
            _mockPizzaShopConfiguration = new Mock<IPizzaShopConfiguration>();
            _mockPizzaCookingTimeCalculator = new Mock<IPizzaCookingTimeCalculator>();
            _mockDelayWrapper = new Mock<IDelayWrapper>();
            _mockPizzaOven = new Mock<IPizzaOven>();

            _mockDelayWrapper.Setup(dw => dw.Delay(It.IsAny<int>())).Callback(() => { });

            var fixture = new Fixture();
            _mockPizzaBaseConfiguration.Setup(pbc => pbc.PizzaBases).Returns(fixture.Create<List<PizzaBase>>());
            _mockToppingsConfiguration.Setup(pbc => pbc.Toppings).Returns(fixture.Create<List<string>>());
            var mockRandomWrapper = new Mock<IRandomWrapper>();
            _mockRandomWrapperBuilder.Setup(pbc => pbc.GetNewRandom()).Returns(mockRandomWrapper.Object);
            _mockPizzaShopConfiguration.Setup(psc => psc.CookingInterval).Returns(0);
        }

        // This test checks the constructors parameters all have guard clauses
        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(PizzaFactory).GetConstructors());
        }

        [Test]
        [AutoData]
        public async Task CreateRandomPizzasAsync_NoPizzasWanted_DoesNothing()
        {
            await Sut.CreateRandomPizzasAsync(0);

            _mockDataRepository.Verify(dr => dr.StorePizzaAsync(It.IsAny<Pizza>()), Times.Never());
            _mockPizzaCookingTimeCalculator.Verify(ctc => ctc.CalculatePizzaCookingTimeMs(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Test]
        [AutoData]
        public async Task CreateRandomPizzasAsync_CreatesCorrectNumberOfPizzas(int noOfPizzas)
        {
            await Sut.CreateRandomPizzasAsync(noOfPizzas);

            _mockDataRepository.Verify(dr => dr.StorePizzaAsync(It.IsAny<Pizza>()), Times.Exactly(noOfPizzas));
            _mockPizzaCookingTimeCalculator.Verify(ctc => ctc.CalculatePizzaCookingTimeMs(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(noOfPizzas));
        }

        [Test]
        [AutoData]
        public async Task CreateRandomPizzasAsync_CallsDelayWrapper(int cookingInterval)
        {
            _mockPizzaShopConfiguration.Setup(psc => psc.CookingInterval).Returns(cookingInterval);
            await Sut.CreateRandomPizzasAsync(1);

            _mockDelayWrapper.Verify(dw => dw.Delay(cookingInterval), Times.Once);
        }

        [Test]
        [AutoData]
        public async Task CreateRandomPizzasAsync_WithValidConfiguration_CreatesCorrectPizza(List<PizzaBase> pizzaBases, List<string> toppings, Generator<int> intGenerator, int cookingTimeMs)
        {
            var chosenToppingId = intGenerator.First(ig => ig <= 2);
            var chosenBaseId = intGenerator.First(ig => ig <= 2);
            var mockRandomWrapper = new Mock<IRandomWrapper>();
            var calls = 0;
            mockRandomWrapper.Setup(rw => rw.GetNextRandom(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                var value = calls == 0 ? chosenBaseId : chosenToppingId;
                calls++;
                return value;
            });
            _mockRandomWrapperBuilder.Setup(wrb => wrb.GetNewRandom()).Returns(mockRandomWrapper.Object);
            _mockPizzaCookingTimeCalculator.Setup(ctc => ctc.CalculatePizzaCookingTimeMs(It.IsAny<string>(), It.IsAny<string>())).Returns(cookingTimeMs);
            _mockPizzaBaseConfiguration.Setup(pbc => pbc.PizzaBases).Returns(pizzaBases);
            _mockToppingsConfiguration.Setup(pbc => pbc.Toppings).Returns(toppings);

            await Sut.CreateRandomPizzasAsync(1);

            var expectedPizza = new Pizza(pizzaBases[chosenBaseId], toppings[chosenToppingId], cookingTimeMs);
            var pizzas = new List<Pizza>();

            using (new AssertionScope())
            {
                _mockDataRepository.Verify(dr => dr.StorePizzaAsync(Capture.In(pizzas)));
                _mockPizzaCookingTimeCalculator.Verify(ctc => ctc.CalculatePizzaCookingTimeMs(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
                pizzas.Should().HaveCount(1);
                pizzas.Single().Should().BeEquivalentTo(expectedPizza);
            }
        }
    }
}
