using AutoFixture;
using Sikoia.Application.Aggregators;
using Sikoia.Application.Tests.DataGenerators;
using Sikoia.Application.Tests.Helpers;
using Sikoia.Contracts.Integration;
using System.Diagnostics.CodeAnalysis;

namespace Sikoia.Application.Tests.Aggregators
{
    [ExcludeFromCodeCoverage]
    public sealed class CompanyStandardAggregatorTests
    {
        private readonly Fixture fixture;
        private readonly CompanyStandardAggregator sut;

        public CompanyStandardAggregatorTests()
        {
            fixture = new Fixture();
            sut = new CompanyStandardAggregator();
        }

        [Theory]
        [ClassData(typeof(SikoiaCompanyStandardInvalidCollectionGenerator))]
        public void Aggregate_WhenInvalidCollectionProvided_ShouldThrowArgumentException(List<SikoiaCompanyStandard>? companies)
        {
            // Arrange
            var expectedErrorMessage = "Parameter: companies must have one or more companies";

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            var exception = Assert.Throws<ArgumentException>(() => sut.Aggregate(companies));
#pragma warning restore CS8604 // Possible null reference argument.

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void Aggregate_WhenSingleCompanyProvided_ShouldReturnReadModel_WithSingleCompany()
        {
            // Arrange
            var companies = new List<SikoiaCompanyStandard>
            {
                fixture.Create<SikoiaCompanyStandard>()
            };

            var expectedCompany = companies[0];

            // Act
            var readModel = sut.Aggregate(companies);

            // Assert
            Assert.NotNull(readModel);
            Assert.False(readModel.HasError);
            Assert.Null(readModel.ErrorMessage);
            Assert.NotNull(readModel.Company);

            Assert.Equal(expectedCompany.CompanyNumber, readModel.Company.CompanyNumber);
            Assert.Equal(expectedCompany.CompanyName, readModel.Company.CompanyName);
            Assert.Equal(expectedCompany.JurisdicationCode, readModel.Company.JurisdicationCode);
            Assert.Equal(expectedCompany.Status, readModel.Company.Status);
            Assert.Equal(expectedCompany.DateEstablished, readModel.Company.DateEstablished);
            Assert.Equal(expectedCompany.DateDisolved, readModel.Company.DateDisolved);
            Assert.Equal(expectedCompany.Address, readModel.Company.Address);
            Assert.Equal(expectedCompany.Activities, readModel.Company.Activities);
            Assert.Equal(expectedCompany.RelatedPeople, readModel.Company.RelatedPeople);
            Assert.Equal(expectedCompany.RelatedCompanies, readModel.Company.RelatedCompanies);
            Assert.Equal(expectedCompany.Officers, readModel.Company.Officers);
            Assert.Equal(expectedCompany.Owners, readModel.Company.Owners);
        }

        [Fact]
        public void Aggregate_WhenMultipleCompaniesProvided_ShouldReturnReadModel_WithMergedCompanyData()
        {
            // Arrange
            var companyNumber = fixture.Create<string>();
            var companyName = fixture.Create<string>();
            var jurisdiction = fixture.Create<string>();
            var companyType = fixture.Create<string>();
            var dateEstablished = fixture.Create<DateTime>();
            var dateDisolved = fixture.Create<DateTime>();
            var address = fixture.Create<SikoiaAddressStandard>();

            var thirdPartyACompany = BuildThirdPartyACompany(
                companyNumber,
                companyName,
                jurisdiction,
                companyType,
                dateEstablished,
                dateDisolved,
                address);

            var thirdPartyBCompany = BuildThirdPartyBCompany(
                companyNumber,
                companyName,
                jurisdiction,
                companyType,
                dateEstablished,
                dateDisolved,
                address);

            var companies = new List<SikoiaCompanyStandard> { thirdPartyACompany, thirdPartyBCompany };

            // Act
            var readModel = sut.Aggregate(companies);

            // Assert
            Assert.NotNull(readModel);
            Assert.False(readModel.HasError);
            Assert.Null(readModel.ErrorMessage);
            Assert.NotNull(readModel.Company);

            Assert.Equal(companyNumber, readModel.Company.CompanyNumber);
            Assert.Equal(companyName, readModel.Company.CompanyName);
            Assert.Equal(jurisdiction, readModel.Company.JurisdicationCode);
            Assert.Equal(thirdPartyACompany.Status, readModel.Company.Status);
            Assert.Equal(dateEstablished, readModel.Company.DateEstablished);
            Assert.Equal(dateDisolved, readModel.Company.DateDisolved);
            Assert.Equal(address, readModel.Company.Address);
            Assert.Equal(thirdPartyBCompany.Activities, readModel.Company.Activities);
            Assert.Equal(thirdPartyBCompany.RelatedPeople, readModel.Company.RelatedPeople);
            Assert.Equal(thirdPartyBCompany.RelatedCompanies, readModel.Company.RelatedCompanies);
            Assert.Equal(thirdPartyACompany.Officers, readModel.Company.Officers);
            Assert.Equal(thirdPartyACompany.Owners, readModel.Company.Owners);
        }

        private SikoiaCompanyStandard BuildThirdPartyACompany(
            string companyNumber,
            string companyName,
            string jurisdiction,
            string companyType,
            DateTime dateEstablished,
            DateTime dateDisolved,
            SikoiaAddressStandard address)
        {
            return NonPublicDataBuilder
                .For<SikoiaCompanyStandard>()
                .With(x => x.CompanyNumber, companyNumber)
                .With(x => x.CompanyName, companyName)
                .With(x => x.JurisdicationCode, jurisdiction)
                .With(x => x.CompanyType, companyType)
                .With(x => x.DateEstablished, dateEstablished)
                .With(x => x.DateDisolved, dateDisolved)
                .With(x => x.Address, address)
                .Without(x => x.Activities)
                .Without(x => x.RelatedPeople)
                .Without(x => x.RelatedCompanies)
                .Create();
        }

        private SikoiaCompanyStandard BuildThirdPartyBCompany(
            string companyNumber,
            string companyName,
            string jurisdiction,
            string companyType,
            DateTime dateEstablished,
            DateTime dateDisolved,
            SikoiaAddressStandard address)
        {
            return NonPublicDataBuilder
                .For<SikoiaCompanyStandard>()
                .With(x => x.CompanyNumber, companyNumber)
                .With(x => x.CompanyName, companyName)
                .With(x => x.JurisdicationCode, jurisdiction)
                .With(x => x.CompanyType, companyType)
                .With(x => x.DateEstablished, dateEstablished)
                .With(x => x.DateDisolved, dateDisolved)
                .With(x => x.Address, address)
                .Without(x => x.CompanyType)
                .Without(x => x.Status)
                .Without(x => x.Officers)
                .Without(x => x.Owners)
                .Create();
        }
    }
}