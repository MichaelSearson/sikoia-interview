using Sikoia.Application.Enums;
using Sikoia.Application.Providers;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;

namespace Sikoia.Application.Services.Jurisdiction
{
    public sealed class BritishJurisdictionService : IJurisdictionService
    {
        private readonly IThirdPartyAHttpProvider thirdPartyAHttpProvider;

        public BritishJurisdictionService(IThirdPartyAHttpProvider thirdPartyAHttpProvider)
        {
            this.thirdPartyAHttpProvider = thirdPartyAHttpProvider;
        }

        public async Task<List<ResultStatus<SikoiaCompanyStandard>>> RetrieveFromAllSources(int companyNumber)
        {
            var result = await thirdPartyAHttpProvider.GetCompanyDataAsync(JurisdictionCode.UK, companyNumber);

            return new List<ResultStatus<SikoiaCompanyStandard>>
            {
                result
            };
        }
    }
}