namespace SimpleJobs.BrasilAPI;

public class CPTECForecastResponse
{
    [JsonPropertyName("cidade")]
    public string? Cidade { get; set; }

    [JsonPropertyName("estado")]
    public string? Estado { get; set; }

    [JsonPropertyName("atualizado_em")]
    public string? AtualizadoEm { get; set; }

    [JsonPropertyName("clima")]
    public List<Clima>? Clima { get; set; }
}

public class Clima
{
    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("condicao")]
    public string? Condicao { get; set; }

    [JsonPropertyName("min")]
    public int? Min { get; set; }

    [JsonPropertyName("max")]
    public int? Max { get; set; }

    [JsonPropertyName("indice_uv")]
    public int? IndiceUV { get; set; }

    [JsonPropertyName("condicao_desc")]
    public string? CondicaoDescricao { get; set; }
}
