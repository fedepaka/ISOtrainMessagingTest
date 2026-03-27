using System.Text.Json.Serialization;

namespace ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel
{
    public class ScimEmail
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "work";

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }
    }
}
