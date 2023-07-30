using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal sealed class OfficialAddressDto
    {
        [JsonProperty("street")]
        public string? Street { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("postcode")]
        public string? Postcode { get; set; }
    }
}