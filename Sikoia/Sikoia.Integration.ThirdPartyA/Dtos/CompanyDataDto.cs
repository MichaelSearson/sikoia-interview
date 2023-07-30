using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal sealed class CompanyDataDto
    {
        [JsonProperty("company_number")]
        public string? CompanyNumber { get; set; }

        [JsonProperty("company_name")]
        public string? CompanyName { get; set; }

        [JsonProperty("jurisdiction_code")]
        public string? JurisdictionCode { get; set; }

        [JsonProperty("company_type")]
        public string? CompanyType { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("date_established")]
        public FullDateDto? DateEstablished { get; set; }

        [JsonProperty("date_dissolved")]
        public FullDateDto? DateDissolved { get; set; }

        [JsonProperty("official_address")]
        public OfficialAddressDto? OfficialAddress { get; set; }

        [JsonProperty("officers")]
        public List<OfficerDto>? Officers { get; set; }

        [JsonProperty("owners")]
        public List<OwnerDto>? Owners { get; set; }
    }
}