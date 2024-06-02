using System.Text.Json.Serialization;

namespace SalessforceAuthPrivatekey;

[JsonSerializable(typeof(string))]
public partial class SFAuthToken : JsonSerializerContext {
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
    [JsonPropertyName("instance_url")]
    public string InstanceUrl { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
    [JsonPropertyName("sfdc_site_url")]
    public string SFDCSiteUrl { get; set; }
    [JsonPropertyName("sfdc_site_id")]
    public string SFDCSiteId { get; set; }
}