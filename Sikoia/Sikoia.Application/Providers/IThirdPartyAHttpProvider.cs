using Sikoia.Application.Enums;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;

namespace Sikoia.Application.Providers
{
    public interface IThirdPartyAHttpProvider
    {
        Task<ResultStatus<SikoiaCompanyStandard>> GetCompanyDataAsync(JurisdictionCode jurisdiction, int companyNumber);
    }
}