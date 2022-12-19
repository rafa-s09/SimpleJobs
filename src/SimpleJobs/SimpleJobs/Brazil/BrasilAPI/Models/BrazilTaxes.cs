namespace SimpleJobs.Brazil.BrasilAPI;

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
