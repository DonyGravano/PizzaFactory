using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Models;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class PizzaBaseConfigurationTests
    {
        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(PizzaBaseConfiguration).GetConstructors());
        }

        [TestCaseSource(nameof(GetInvalidOptions))]
        public void Ctor_NullOrEmprtPizzaBaseList_ThrowsInvalidOperationException(IReadOnlyList<PizzaBase> pizzaBases)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {
               new KeyValuePair<string, string>("PizzaBases", JsonSerializer.Serialize(pizzaBases))
            });

            Action sut = () => new PizzaBaseConfiguration(configurationBuilder.Build());

            sut.Should().Throw<InvalidOperationException>();
        }

        [Test]
        [AutoData]
        public void Ctor_ValidPizzaBaseList_BindsCorrectly(IReadOnlyList<PizzaBase> pizzaBases)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var data = new List<KeyValuePair<string, string>>
            {
               new KeyValuePair<string, string>("PizzaBases:0:Type", pizzaBases[0].Type),
               new KeyValuePair<string, string>("PizzaBases:0:CookingTimeMultiplier", pizzaBases[0].CookingTimeMultiplier.ToString()),
               new KeyValuePair<string, string>("PizzaBases:1:Type", pizzaBases[1].Type),
               new KeyValuePair<string, string>("PizzaBases:1:CookingTimeMultiplier", pizzaBases[1].CookingTimeMultiplier.ToString()),
               new KeyValuePair<string, string>("PizzaBases:2:Type", pizzaBases[2].Type),
               new KeyValuePair<string, string>("PizzaBases:2:CookingTimeMultiplier", pizzaBases[2].CookingTimeMultiplier.ToString()),
            };
            configurationBuilder.AddInMemoryCollection(data);

            var sut = new PizzaBaseConfiguration(configurationBuilder.Build());

            sut.PizzaBases.Should().BeEquivalentTo(pizzaBases);
        }

        private static IEnumerable<IReadOnlyList<PizzaBase>> GetInvalidOptions()
        {
            return new List<IReadOnlyList<PizzaBase>> {
                null,
                new List<PizzaBase>()
            };    
        }
    }
}
