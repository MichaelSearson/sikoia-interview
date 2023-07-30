using Sikoia.Application.ReadModels;
using Sikoia.Contracts.Integration;

namespace Sikoia.Application.Aggregators
{
    public sealed class CompanyStandardAggregator : ICompanyStandardAggregator
    {
        public CompanyReadModel Aggregate(List<SikoiaCompanyStandard> companies)
        {
            if (companies is null || !companies.Any())
            {
                throw new ArgumentException($"Parameter: {nameof(companies)} must have one or more companies");
            }

            if (companies.Count == 1)
            {
                return new CompanyReadModel(companies[0]);
            }

            // Naively assumes that all sources of data will have the same values for duplicate
            // properties and that we only care about non-null sources of data like "Activities"
            // which we know will only be sourced from specific third parties.
            var duplicateCompanyData = companies.First();
            var companyStatus = companies.FirstOrDefault(x => !string.IsNullOrEmpty(x.Status))?.Status;
            var activities = companies.FirstOrDefault(x => x.Activities != null && x.Activities.Any())?.Activities;
            var relatedPeople = companies.FirstOrDefault(x => x.RelatedPeople != null && x.RelatedPeople.Any())?.RelatedPeople;
            var relatedCompanies = companies.FirstOrDefault(x => x.RelatedCompanies != null && x.RelatedCompanies.Any())?.RelatedCompanies;
            var officers = companies.FirstOrDefault(x => x.Officers != null && x.Officers.Any())?.Officers;
            var owners = companies.FirstOrDefault(x => x.Owners != null && x.Owners.Any())?.Owners;

            var mergedStandardCompany = new SikoiaCompanyStandard(
                duplicateCompanyData.CompanyNumber,
                duplicateCompanyData.CompanyName,
                duplicateCompanyData.JurisdicationCode,
                duplicateCompanyData.CompanyType,
                companyStatus,
                duplicateCompanyData.DateEstablished,
                duplicateCompanyData.DateDisolved,
                duplicateCompanyData.Address,
                activities,
                relatedPeople,
                relatedCompanies,
                officers,
                owners);

            return new CompanyReadModel(mergedStandardCompany);
        }
    }
}