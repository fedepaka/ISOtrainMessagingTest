using System.Text.Json.Serialization;

namespace ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel
{
    public class ScimMeta
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "User";

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
