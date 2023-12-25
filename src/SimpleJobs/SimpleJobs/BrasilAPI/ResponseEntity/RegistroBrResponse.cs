namespace SimpleJobs.BrasilAPI;

public class RegistroBrResponse
{
    [JsonPropertyName("status_code")]
    public int? StatusCode { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("fqdn")]
    public string? Fqdn { get; set; }

    [JsonPropertyName("fqdnace")]
    public string? Fqdnace { get; set; }

    [JsonPropertyName("exempt")]
    public bool? Exempt { get; set; }

    [JsonPropertyName("hosts"), JsonIgnore]
    public List<string>? Hosts { get; set; }

    [JsonPropertyName("publication-status"), JsonIgnore]
    public string? PublicationStatus { get; set; }

    [JsonPropertyName("expires-at"), JsonIgnore]
    public string? ExpiresAt { get; set; }

    [JsonPropertyName("suggestions"), JsonIgnore]
    public List<string>? Suggestions { get; set; }
}
