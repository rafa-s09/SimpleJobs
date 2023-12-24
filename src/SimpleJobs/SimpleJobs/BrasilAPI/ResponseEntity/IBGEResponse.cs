namespace SimpleJobs.BrasilAPI;

public class IBGEResponse
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("sigla")]
    public string? Sigla { get; set; }

    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("regiao")]
    public IbgeRegiao? Regiao { get; set; }
}

public class IbgeRegiao
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("sigla")]
    public string? Sigla { get; set; }

    [JsonPropertyName("nome")]
    public string? Nome { get; set; }
}
