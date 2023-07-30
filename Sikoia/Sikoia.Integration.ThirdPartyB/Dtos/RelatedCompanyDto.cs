using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyB.Dtos
{
    internal sealed class RelatedCompanyDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("dateFrom")]
        public string? DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public string? DateTo { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("ownership")]
        public decimal? Ownership { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }
    }
}