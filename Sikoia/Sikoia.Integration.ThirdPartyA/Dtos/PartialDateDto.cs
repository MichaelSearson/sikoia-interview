using Newtonsoft.Json;

namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal sealed class PartialDateDto
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }
    }
}