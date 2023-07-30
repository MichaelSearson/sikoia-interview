using Sikoia.Application.Aggregators;
using Sikoia.Application.Factories;
using Sikoia.Application.ReadModels;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;
using System.Text;

namespace Sikoia.Application.Queries.Company
{
    public sealed class FindCompanyByJurisdictionQueryHandler : IAsyncQueryHandler<FindCompanyByJurisdictionQuery, CompanyReadModel>
    {
        private readonly IJurisdictionServiceFactory jurisdictionServiceFactory;
        private readonly ICompanyStandardAggregator companyStandardAggregator;

        public FindCompanyByJurisdictionQueryHandler(
            IJurisdictionServiceFactory jurisdictionServiceFactory,
            ICompanyStandardAggregator companyStandardAggregator)
        {
            this.jurisdictionServiceFactory = jurisdictionServiceFactory;
            this.companyStandardAggregator = companyStandardAggregator;
        }

        public async Task<CompanyReadModel> HandleAsync(FindCompanyByJurisdictionQuery query)
        {
            var service = jurisdictionServiceFactory.GetJurisdiction(query.Jurisdication);

            var result = await service.RetrieveFromAllSources(query.CompanyNumber);

            if (result.All(x => !x.Success))
            {
                return new CompanyReadModel(ComposeErrorMessage(result));
            }

            // Note we are silently ignoring cases where we have some errors - e.g. ThirdPartyB
            // times out but ThirdPatyA succeeds. In which case we just return all the other data we
            // have gathered. We should probably tell the user that they are looking at an incomplete
            // snapshot though.
            var successfullyRetrievedData = result
                .Where(x => x.Success)
                .Select(x => x.Result)
                .ToList();

            return companyStandardAggregator.Aggregate(successfullyRetrievedData!);
        }

        private static string ComposeErrorMessage(List<ResultStatus<SikoiaCompanyStandard>> results)
        {
            var errorMessageBuilder = new StringBuilder();
            foreach (var error in results.Where(x => !x.Success && !string.IsNullOrEmpty(x.Error)))
            {
                errorMessageBuilder.Append(error.Error);
                errorMessageBuilder.Append(' ');
            }

            return errorMessageBuilder.ToString().Trim();
        }
    }
}