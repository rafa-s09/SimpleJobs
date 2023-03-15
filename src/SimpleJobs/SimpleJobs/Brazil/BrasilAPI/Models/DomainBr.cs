namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Avalia um dominio no registro.br
/// </summary>
public class DomainBr
{
    /// <summary>
    /// Field Name: status_code
    /// </summary>
    [JsonPropertyName("status_code")]
    public int? StatusCode { get; set; }

    /// <summary>
    /// Field Name: status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Field Name: fqdn
    /// </summary>
    [JsonPropertyName("fqdn")]
    public string? Fqdn { get; set; }

    /// <summary>
    /// Field Name: fqdnace
    /// </summary>
    [JsonPropertyName("fqdnace")]
    public string? Fqdnace { get; set; }

    /// <summary>
    /// Field Name: exempt
    /// </summary>
    [JsonPropertyName("exempt")]
    public bool? Exempt { get; set; }

    /// <summary>
    /// Field Name: hosts
    /// </summary>
    [JsonPropertyName("hosts"), JsonIgnore]
    public List<string>? Hosts { get; set; }

    /// <summary>
    /// Field Name: publication-status
    /// </summary>
    [JsonPropertyName("publication-status"), JsonIgnore]
    public string? PublicationStatus { get; set; }

    /// <summary>
    /// Field Name: expires-at
    /// </summary>
    [JsonPropertyName("expires-at"), JsonIgnore]
    public string? ExpiresAt { get; set; }

    /// <summary>
    /// Field Name: suggestions
    /// </summary>
    [JsonPropertyName("suggestions"), JsonIgnore]
    public List<string>? Suggestions { get; set; }
}
