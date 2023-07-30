using Moq;
using Sikoia.Application.Enums;
using Sikoia.Application.Providers;
using Sikoia.Application.Services.Jurisdiction;
using System.Diagnostics.CodeAnalysis;

namespace Sikoia.Application.Factories.Factories
{
    [ExcludeFromCodeCoverage]
    public sealed class JurisdictionServiceFactoryTests
    {
        private readonly Mock<IServiceProvider> serviceProvider;
        private readonly JurisdictionServiceFactory sut;

        public JurisdictionServiceFactoryTests()
        {
            serviceProvider = new Mock<IServiceProvider>();

            sut = new JurisdictionServiceFactory(serviceProvider.Object);
        }

        [Fact]
        public void GetJurisdiction_WhenBritishJurisdictionProvided_ShouldRetrieveBritishService()
        {
            // Arrange
            var thirdPartyA = new Mock<IThirdPartyAHttpProvider>();
            var result = new BritishJurisdictionService(thirdPartyA.Object);

            serviceProvider.Setup(x => x.GetService(It.Is<Type>(x => x == typeof(BritishJurisdictionService)))).Returns(result);

            // Act
            var service = sut.GetJurisdiction(JurisdictionCode.UK);

            // Assert
            Assert.NotNull(service);
            serviceProvider.Verify(x => x.GetService(It.IsAny<Type>()), Times.Once);
        }

        [Fact]
        public void GetJurisdiction_WhenDutchJurisdictionProvided_ShouldRetrieveDutchService()
        {
            // Arrange
            var thirdPartyB = new Mock<IThirdPartyBHttpProvider>();
            var result = new DutchJurisdictionService(thirdPartyB.Object);

            serviceProvider.Setup(x => x.GetService(It.Is<Type>(x => x == typeof(DutchJurisdictionService)))).Returns(result);

            // Act
            var service = sut.GetJurisdiction(JurisdictionCode.NL);

            // Assert
            Assert.NotNull(service);
            serviceProvider.Verify(x => x.GetService(It.IsAny<Type>()), Times.Once);
        }

        [Fact]
        public void GetJurisdiction_WhenGermanJurisdictionProvided_ShouldRetrieveGermanService()
        {
            // Arrange
            var thirdPartyA = new Mock<IThirdPartyAHttpProvider>();
            var thirdPartyB = new Mock<IThirdPartyBHttpProvider>();
            var result = new GermanJurisdictionService(thirdPartyA.Object, thirdPartyB.Object);

            serviceProvider.Setup(x => x.GetService(It.Is<Type>(x => x == typeof(GermanJurisdictionService)))).Returns(result);

            // Act
            var service = sut.GetJurisdiction(JurisdictionCode.DE);

            // Assert
            Assert.NotNull(service);
            serviceProvider.Verify(x => x.GetService(It.IsAny<Type>()), Times.Once);
        }

        [Fact]
        public void GetJurisdiction_WhenJurisdictionServiceCannotBeResolved_ShouldThrowNullReferenceException()
        {
            // Arrange
            const string expectedErrorMessage = "Could not resolve a service for the provided jurisdiction: DE. This should never happen.";
            serviceProvider.Setup(x => x.GetService(It.Is<Type>(x => x == typeof(GermanJurisdictionService)))).Returns(null);

            // Act
            var exception = Assert.Throws<NullReferenceException>(() => sut.GetJurisdiction(JurisdictionCode.DE));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }
}