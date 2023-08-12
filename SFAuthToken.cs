using System.Text.Json.Serialization;
#nullable disable
public record SFAuthToken
{
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
}