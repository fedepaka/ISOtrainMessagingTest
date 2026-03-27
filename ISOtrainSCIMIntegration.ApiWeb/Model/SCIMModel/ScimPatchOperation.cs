using System.Text.Json.Serialization;

namespace ISOtrainSCIMIntegration.ApiWeb.Model.SCIMModel
{
    public class ScimPatchOperation
    {
        [JsonPropertyName("op")]
        public string Op { get; set; } // Puede ser "add", "remove" o "replace"

        [JsonPropertyName("path")]
        public string Path { get; set; } // El atributo a modificar, ej: "active" o "name.familyName"

        [JsonPropertyName("value")]
        public object Value { get; set; } // El nuevo valor (puede ser un string, bool o un objeto complejo)
    }
}
