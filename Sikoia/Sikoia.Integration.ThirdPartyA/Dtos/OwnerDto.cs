using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal sealed class OwnerDto
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

        [JsonProperty("ownership_type")]
        public string? OwnershipType { get; set; }

        [JsonProperty("shares_held")]
        public decimal? SharesHeld { get; set; }

        [JsonProperty("date_of_birth")]
        public PartialDateDto? DateOfBirth { get; set; }
    }
}