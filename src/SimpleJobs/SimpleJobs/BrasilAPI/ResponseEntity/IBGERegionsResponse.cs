namespace SimpleJobs.BrasilAPI;

public class IBGERegionsResponse
{
    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("codigo_ibge")]
    public string? Codigo { get; set; }
}