using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal sealed class OfficerDto
    {
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("middlenames")]
        public string? MiddleNames { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("date_from")]
        public FullDateDto? DateFrom { get; set; }

        [JsonProperty("date_to")]
        public FullDateDto? DateTo { get; set; }

        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("date_of_birth")]
        public PartialDateDto? DateOfBirth { get; set; }
    }
}