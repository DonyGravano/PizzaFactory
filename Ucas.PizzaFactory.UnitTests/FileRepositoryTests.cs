using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Ucas.PizzaFactory.Interfaces;

namespace Ucas.PizzaFactory.UnitTests
{
    [TestFixture]
    public class FileRepositoryTests
    {
        // This test checks the constructors parameters all have guard clauses
        [Test]
        public void Ctor_NullArgument_ThrowsNullArgumentException()
        {
            var fixture = new Fixture();
            var assertion = new GuardClauseAssertion(fixture.Customize(new AutoMoqCustomization()));
            assertion.Verify(typeof(FileRepository).GetConstructors());
        }

        [Test]
        public async Task StorePizzaAsync_NullArgument_ThrowsNullArgumentException()
        {
            var fileRepo = new FileRepository(new Mock<IPizzaShopConfiguration>().Object);

            Func<Task> sut = async () => await fileRepo.StorePizzaAsync(null);

            await sut.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
