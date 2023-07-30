using Sikoia.Contracts.Integration;
using Sikoia.Integration.ThirdPartyA.Dtos;
using System.Text;

namespace Sikoia.Integration.ThirdPartyA.Mapping
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
                dto.JurisdictionCode,
                dto.CompanyType,
                dto.Status,
                ParseFullDateDto(dto.DateEstablished),
                ParseFullDateDto(dto.DateDissolved),
                ParseOfficialAddress(dto.OfficialAddress),
                null,
                null,
                null,
                ParseOfficers(dto.Officers),
                ParseOwners(dto.Owners));
        }

        private static DateTime? ParseFullDateDto(FullDateDto? fullDateDto)
        {
            if (fullDateDto is null)
            {
                return null;
            }

            var dateTimeBuilder = new StringBuilder();
            dateTimeBuilder.Append(fullDateDto.Year);
            dateTimeBuilder.Append('/');
            dateTimeBuilder.Append(fullDateDto.Month);
            dateTimeBuilder.Append('/');
            dateTimeBuilder.Append(fullDateDto.Day);

            return DateTime.Parse(dateTimeBuilder.ToString());
        }

        private static SikoiaAddressStandard? ParseOfficialAddress(OfficialAddressDto? officialAddressDto)
        {
            if (officialAddressDto is null)
            {
                throw new ArgumentNullException(nameof(officialAddressDto), $"{nameof(OfficialAddressDto)} parameter should never be null when mapping");
            }

            return new SikoiaAddressStandard(
                officialAddressDto.Street,
                officialAddressDto.City,
                officialAddressDto.Country,
                officialAddressDto.Postcode);
        }

        private static List<SikoiaOfficerStandard>? ParseOfficers(List<OfficerDto>? dtos)
        {
            if (dtos is null)
            {
                return null;
            }

            return dtos
                .Select(x => new SikoiaOfficerStandard(
                    x.FirstName,
                    x.MiddleNames,
                    x.LastName,
                    x.Name,
                    ParseFullDateDto(x.DateFrom),
                    ParseFullDateDto(x.DateTo),
                    x.Role,
                    ParsePartialDate(x.DateOfBirth)))
                .ToList();
        }

        private static List<SikoiaOwnerStandard>? ParseOwners(List<OwnerDto>? dtos)
        {
            if (dtos is null)
            {
                return null;
            }

            return dtos
                .Select(x => new SikoiaOwnerStandard(
                    x.FirstName,
                    x.MiddleNames,
                    x.LastName,
                    x.Name,
                    ParseFullDateDto(x.DateFrom),
                    ParseFullDateDto(x.DateTo),
                    x.OwnershipType,
                    x.SharesHeld ?? decimal.Zero,
                    ParsePartialDate(x.DateOfBirth)))
                .ToList();
        }

        private static SikoiaPartialDateStandard? ParsePartialDate(PartialDateDto? partialDateDto)
        {
            if (partialDateDto is null)
            {
                return null;
            }

            return new SikoiaPartialDateStandard(partialDateDto.Year, partialDateDto.Month);
        }
    }
}