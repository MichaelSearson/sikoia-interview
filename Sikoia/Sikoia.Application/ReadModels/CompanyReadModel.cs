using Sikoia.Contracts.Integration;

namespace Sikoia.Application.ReadModels
{
    public sealed class CompanyReadModel
    {
        public CompanyReadModel(SikoiaCompanyStandard company)
        {
            Company = company;
        }

        public CompanyReadModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public SikoiaCompanyStandard? Company { get; }

        public string? ErrorMessage { get; }

        public bool HasError => ErrorMessage != null;
    }
}