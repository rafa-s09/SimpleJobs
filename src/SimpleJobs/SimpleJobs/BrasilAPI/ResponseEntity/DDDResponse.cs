namespace SimpleJobs.BrasilAPI;

public class DDDResponse
{
    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("cities")]
    public List<string>? Cities { get; set; }

    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("regiao")]
    public Regiao? Regiao { get; set; }
}

public class Regiao
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("sigla")]
    public string? Sigla { get; set; }

    [JsonPropertyName("nome")]
    public string? Nome { get; set; }
}
