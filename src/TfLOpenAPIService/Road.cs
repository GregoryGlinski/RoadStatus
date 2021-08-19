using System.Text.Json.Serialization;

namespace TfLOpenApiService
{
    public class Road
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("statusSeverity")]
        public string StatusSeverity { get; set; }

        [JsonPropertyName("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        [JsonPropertyName("bounds")]
        public string Bounds { get; set; }

        [JsonPropertyName("envelope")]
        public string Envelope { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
