namespace SimpleJobs.BrasilAPI;

public class PIXResponse
{
    [JsonPropertyName("ispb")]
    public string? ISPB { get; set; }

    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("nome_reduzido")]
    public string? NomeReduzido { get; set; }

    [JsonPropertyName("modalidade_participacao")]
    public string? Modalidade { get; set; }

    [JsonPropertyName("tipo_participacao")]
    public string? TipoParticipacao { get; set; }

    [JsonPropertyName("inicio_operacao")]
    public string? InicioOperacao { get; set; }
}
