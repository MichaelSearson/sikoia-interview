using Sikoia.Application.Enums;
using Sikoia.Application.ReadModels;

namespace Sikoia.Application.Queries.Company
{
    public sealed class FindCompanyByJurisdictionQuery : IQuery<CompanyReadModel>, IEquatable<FindCompanyByJurisdictionQuery>
    {
        public FindCompanyByJurisdictionQuery(JurisdictionCode jurisdication, int companyNumber)
        {
            Jurisdication = jurisdication;
            CompanyNumber = companyNumber;
        }

        public JurisdictionCode Jurisdication { get; }

        public int CompanyNumber { get; }

        public bool Equals(FindCompanyByJurisdictionQuery? other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Jurisdication == other.Jurisdication && CompanyNumber == other.CompanyNumber;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as FindCompanyByJurisdictionQuery);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Jurisdication, CompanyNumber);
        }
    }
}