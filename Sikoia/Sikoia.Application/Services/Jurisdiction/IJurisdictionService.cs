using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;

namespace Sikoia.Application.Services.Jurisdiction
{
    public interface IJurisdictionService
    {
        Task<List<ResultStatus<SikoiaCompanyStandard>>> RetrieveFromAllSources(int companyNumber);
    }
}