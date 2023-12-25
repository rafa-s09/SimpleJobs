namespace SimpleJobs.BrasilAPI;

public class TaxasResponse
{
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    [JsonPropertyName("valor")]
    public decimal? Value { get; set; }
}
