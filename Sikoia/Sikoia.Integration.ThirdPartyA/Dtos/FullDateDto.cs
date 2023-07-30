using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal sealed class FullDateDto
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("day")]
        public int Day { get; set; }
    }
}