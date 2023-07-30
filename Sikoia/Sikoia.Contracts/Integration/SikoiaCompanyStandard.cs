namespace Sikoia.Contracts.Integration
{
    public sealed class SikoiaCompanyStandard
    {
        public SikoiaCompanyStandard(
            string? companyNumber,
            string? companyName,
            string? jurisdicationCode,
            string? companyType,
            string? status,
            DateTime? dateEstablished,
            DateTime? dateDisolved,
            SikoiaAddressStandard? address,
            List<SikoiaActivityStandard>? activities,
            List<SikoiaRelatedPersonStandard>? relatedPeople,
            List<SikoiaRelatedCompanyStandard>? relatedCompanies,
            List<SikoiaOfficerStandard>? officers,
            List<SikoiaOwnerStandard>? owners)
        {
            CompanyNumber = companyNumber;
            CompanyName = companyName;
            JurisdicationCode = jurisdicationCode;
            CompanyType = companyType;
            Status = status;
            DateEstablished = dateEstablished;
            DateDisolved = dateDisolved;
            Address = address;
            Activities = activities;
            RelatedPeople = relatedPeople;
            RelatedCompanies = relatedCompanies;
            Officers = officers;
            Owners = owners;
        }

        public string? CompanyNumber { get; }

        public string? CompanyName { get; }

        public string? JurisdicationCode { get; }

        public string? CompanyType { get; }

        public string? Status { get; }

        public DateTime? DateEstablished { get; }

        public DateTime? DateDisolved { get; }

        public SikoiaAddressStandard? Address { get; }

        public List<SikoiaActivityStandard>? Activities { get; }

        public List<SikoiaRelatedPersonStandard>? RelatedPeople { get; }

        public List<SikoiaRelatedCompanyStandard>? RelatedCompanies { get; }

        public List<SikoiaOfficerStandard>? Officers { get; }

        public List<SikoiaOwnerStandard>? Owners { get; }
    }
}