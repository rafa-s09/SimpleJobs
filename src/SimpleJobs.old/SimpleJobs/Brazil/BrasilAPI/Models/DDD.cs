namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Information related to DDDs
/// </summary>
public class DDD
{
    /// <summary>
    /// Field Name: state
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Field Name: cities
    /// </summary>
    [JsonPropertyName("cities")]
    public List<string>? Cities { get; set; }

    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    /// <summary>
    /// Field Name: regiao
    /// </summary>
    [JsonPropertyName("regiao")]
    public Region? Region { get; set; }
}

/// <summary>
/// Information related to DDDs
/// </summary>
public class Region
{
    /// <summary>
    /// Field Name: id
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Field Name: sigla
    /// </summary>
    [JsonPropertyName("sigla")]
    public string? Acronym { get; set; }

    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }
}
