namespace SimpleJobs.BrasilAPI;

public class CPTECOceanResponse
{
    [JsonPropertyName("cidade")]
    public string? Cidade { get; set; }

    [JsonPropertyName("estado")]
    public string? Estado { get; set; }

    [JsonPropertyName("atualizado_em")]
    public string? AtualizadoEm { get; set; }

    [JsonPropertyName("ondas")]
    public List<Ondas>? Ondas { get; set; }
}

public class Ondas
{
    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("dados_ondas")]
    public List<DadosOndas>? DadosOndas { get; set; }

}

public class DadosOndas
{
    [JsonPropertyName("vento")]
    public float? Vento { get; set; }

    [JsonPropertyName("direcao_vento")]
    public string? DirecaoVento { get; set; }

    [JsonPropertyName("direcao_vento_desc")]
    public string? DirecaoVentoDescricao { get; set; }

    [JsonPropertyName("altura_onda")]
    public float? AlturaOnda { get; set; }

    [JsonPropertyName("direcao_onda")]
    public string? DirecaoOnda { get; set; }

    [JsonPropertyName("direcao_onda_desc")]
    public string? DirecaoOndaDescricao { get; set; }

    [JsonPropertyName("agitacao")]
    public string? Agitacao { get; set; }

    [JsonPropertyName("hora")]
    public string? Hora { get; set; }
}
