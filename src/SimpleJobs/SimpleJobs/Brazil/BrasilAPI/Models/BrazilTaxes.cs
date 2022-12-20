namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Interest tax and official indices
/// </summary>
public class BrazilTaxes
{
    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    /// <summary>
    /// Field Name: valor
    /// </summary>
    [JsonPropertyName("valor")]
    public decimal? Value { get; set; }

}
