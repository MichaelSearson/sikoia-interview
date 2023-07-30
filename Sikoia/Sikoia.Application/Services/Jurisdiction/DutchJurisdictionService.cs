using Sikoia.Application.Enums;
using Sikoia.Application.Providers;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;

namespace Sikoia.Application.Services.Jurisdiction
{
    public sealed class DutchJurisdictionService : IJurisdictionService
    {
        private readonly IThirdPartyBHttpProvider thirdPartyBHttpProvider;

        public DutchJurisdictionService(IThirdPartyBHttpProvider thirdPartyBHttpProvider)
        {
            this.thirdPartyBHttpProvider = thirdPartyBHttpProvider;
        }

        public async Task<List<ResultStatus<SikoiaCompanyStandard>>> RetrieveFromAllSources(int companyNumber)
        {
            var result = await thirdPartyBHttpProvider.GetCompanyDataAsync(JurisdictionCode.NL, companyNumber);

            return new List<ResultStatus<SikoiaCompanyStandard>>
            {
                result
            };
        }
    }
}