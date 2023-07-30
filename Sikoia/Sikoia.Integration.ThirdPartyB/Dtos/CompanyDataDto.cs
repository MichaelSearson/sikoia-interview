using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyB.Dtos
{
    internal sealed class CompanyDataDto
    {
        [JsonProperty("companyNumber")]
        public string? CompanyNumber { get; set; }

        [JsonProperty("companyName")]
        public string? CompanyName { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("dateFrom")]
        public string? DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public string? DateTo { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("activities")]
        public List<ActivityDto>? Activities { get; set; }

        [JsonProperty("relatedPersons")]
        public List<RelatedPersonDto>? RelatedPeople { get; set; }

        [JsonProperty("relatedCompanies")]
        public List<RelatedCompanyDto>? RelatedCompanies { get; set; }
    }
}