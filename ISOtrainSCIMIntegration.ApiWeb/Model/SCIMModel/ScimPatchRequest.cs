using System.Text.Json.Serialization;

namespace ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel
{
    public class ScimPatchRequest
    {
        [JsonPropertyName("schemas")]
        public List<string> Schemas { get; set; } = new() { "urn:ietf:params:scim:api:messages:2.0:PatchOp" };

        [JsonPropertyName("Operations")]
        public List<ScimPatchOperation> Operations { get; set; }
    }
}
