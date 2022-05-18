using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class ToppingsConfigurationTests
    {
        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(ToppingsConfiguration).GetConstructors());
        }

        [TestCaseSource(nameof(GetInvalidOptions))]
        public void Ctor_NullOrEmprtToppingsList_ThrowsInvalidOperationException(IReadOnlyList<string> toppings)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {
               new KeyValuePair<string, string>("Toppings", JsonSerializer.Serialize(toppings))
            });

            Action sut = () => new ToppingsConfiguration(configurationBuilder.Build());

            sut.Should().Throw<InvalidOperationException>();
        }

        [Test]
        [AutoData]
        public void Ctor_ValidToppingsList_BindsCorrectly(IReadOnlyList<string> toppings)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var data = new List<KeyValuePair<string, string>>
            {
               new KeyValuePair<string, string>("Toppings:0", toppings[0]),
               new KeyValuePair<string, string>("Toppings:1", toppings[1]),
               new KeyValuePair<string, string>("Toppings:2", toppings[2]),
            };
            configurationBuilder.AddInMemoryCollection(data);

            var sut = new ToppingsConfiguration(configurationBuilder.Build());

            sut.Toppings.Should().BeEquivalentTo(toppings);
        }

        private static IEnumerable<IReadOnlyList<string>> GetInvalidOptions()
        {
            return new List<IReadOnlyList<string>> {
                null,
                new List<string>()
            };
        }
    }
}
