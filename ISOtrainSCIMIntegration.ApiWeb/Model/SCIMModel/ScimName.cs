using System.Text.Json.Serialization;

namespace ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel
{
    public class ScimName
    {
        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }

        [JsonPropertyName("formatted")]
        public string Formatted => $"{GivenName} {FamilyName}";
    }
}
