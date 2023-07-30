using Sikoia.Application.Enums;
using Sikoia.Application.Providers;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;

namespace Sikoia.Application.Services.Jurisdiction
{
    public sealed class GermanJurisdictionService : IJurisdictionService
    {
        private readonly IThirdPartyAHttpProvider thirdPartyAHttpProvider;
        private readonly IThirdPartyBHttpProvider thirdPartyBHttpProvider;

        public GermanJurisdictionService(IThirdPartyAHttpProvider thirdPartyAHttpProvider, IThirdPartyBHttpProvider thirdPartyBHttpProvider)
        {
            this.thirdPartyAHttpProvider = thirdPartyAHttpProvider;
            this.thirdPartyBHttpProvider = thirdPartyBHttpProvider;
        }

        public async Task<List<ResultStatus<SikoiaCompanyStandard>>> RetrieveFromAllSources(int companyNumber)
        {
            var partyATask = thirdPartyAHttpProvider.GetCompanyDataAsync(JurisdictionCode.DE, companyNumber);
            var partyBTask = thirdPartyBHttpProvider.GetCompanyDataAsync(JurisdictionCode.DE, companyNumber);

            await Task.WhenAll(partyATask, partyBTask);

            return new List<ResultStatus<SikoiaCompanyStandard>>
            {
                partyATask.Result,
                partyBTask.Result
            };
        }
    }
}