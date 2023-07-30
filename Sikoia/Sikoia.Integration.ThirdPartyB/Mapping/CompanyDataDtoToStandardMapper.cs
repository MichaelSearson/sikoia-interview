using Sikoia.Contracts.Integration;
using Sikoia.Integration.ThirdPartyB.Dtos;

namespace Sikoia.Integration.ThirdPartyB.Mapping
{
    internal static class CompanyDataDtoToStandardMapper
    {
        public static SikoiaCompanyStandard Map(CompanyDataDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"{nameof(CompanyDataDto)} parameter should never be null when mapping");
            }

            return new SikoiaCompanyStandard(
                dto.CompanyNumber,
                dto.CompanyName,
                dto.Country,
                null,
                null,
                ParseDateTime(dto.DateFrom),
                ParseDateTime(dto.DateTo),
                ParseAddress(dto.Address),
                ParseActivities(dto.Activities),
                ParseRelatedPeople(dto.RelatedPeople),
                ParseRelatedCompanies(dto.RelatedCompanies),
                null,
                null);
        }

        private static DateTime? ParseDateTime(string? date)
        {
            if (date is null)
            {
                return null;
            }

            return DateTime.Parse(date);
        }

        private static SikoiaAddressStandard? ParseAddress(string? address)
        {
            if (address is null)
            {
                return null;
            }

            // Naive assumption that format of address will always be comma separated and have four parts
            // just to demonstrate mapping. Obviously this should be more robust.
            var addressParts = address.Split(',');

            return new SikoiaAddressStandard(
                addressParts[0],
                addressParts[1],
                addressParts[2],
                addressParts[3]);
        }

        private static List<SikoiaActivityStandard>? ParseActivities(List<ActivityDto>? activities)
        {
            if (activities is null)
            {
                return null;
            }

            return activities
                .Select(x => new SikoiaActivityStandard(x.ActivityCode ?? 0, x.ActivityDescription))
                .ToList();
        }

        private static List<SikoiaRelatedPersonStandard>? ParseRelatedPeople(List<RelatedPersonDto>? relatedPeople)
        {
            if (relatedPeople is null)
            {
                return null;
            }

            return relatedPeople
                .Select(x => new SikoiaRelatedPersonStandard(
                    x.Name,
                    ParseDateTime(x.DateFrom),
                    ParseDateTime(x.DateTo),
                    x.Type,
                    x.Ownership,
                    ParseDateTime(x.BirthDate),
                    x.Nationality))
                .ToList();
        }

        private static List<SikoiaRelatedCompanyStandard>? ParseRelatedCompanies(List<RelatedCompanyDto>? relatedCompanies)
        {
            if (relatedCompanies is null)
            {
                return null;
            }

            return relatedCompanies
                .Select(x => new SikoiaRelatedCompanyStandard(
                    x.Name,
                    ParseDateTime(x.DateFrom),
                    ParseDateTime(x.DateTo),
                    x.Type,
                    x.Ownership,
                    x.Country))
                .ToList();
        }
    }
}