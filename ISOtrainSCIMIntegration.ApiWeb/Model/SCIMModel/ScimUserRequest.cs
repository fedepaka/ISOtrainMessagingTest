using System.Text.Json.Serialization;

namespace ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel
{
    /// <summary>
    /// Esta clase representa el JSON que envía Azure AD o Okta. Usamos JsonPropertyName para asegurar la compatibilidad sin romper las convenciones de C#.
    /// </summary>
    public class ScimUserRequest
    {
        [JsonPropertyName("schemas")]
        public List<string> Schemas { get; set; } = new() { "urn:ietf:params:scim:schemas:core:2.0:User" };

        [JsonPropertyName("id")]
        public string? Id { get; set; } // El ID en tu sistema

        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; } // El ID que viene del IdP (Azure/Okta)

        [JsonPropertyName("userName")]
        public string UserName { get; set; } // Generalmente el Email

        [JsonPropertyName("name")]
        public ScimName Name { get; set; }

        [JsonPropertyName("emails")]
        public List<ScimEmail> Emails { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("meta")]
        public ScimMeta? Meta { get; set; }
    }
}
