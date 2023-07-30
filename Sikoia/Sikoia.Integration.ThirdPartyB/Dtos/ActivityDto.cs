using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyB.Dtos
{
    internal sealed class ActivityDto
    {
        [JsonProperty("activityCode")]
        public int? ActivityCode { get; set; }

        [JsonProperty("activityDescription")]
        public string? ActivityDescription { get; set; }
    }
}