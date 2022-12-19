namespace SimpleJobs.Brazil.BrasilAPI;

public class Ncm
{
    /// <summary>
    /// Field Name: codigo
    /// </summary>
    [JsonPropertyName("codigo")]
    public string? Code { get; set; }

    /// <summary>
    /// Field Name: descricao
    /// </summary>
    [JsonPropertyName("descricao")]
    public string? Description { get; set; }

    /// <summary>
    /// Field Name: data_inicio
    /// </summary>
    [JsonPropertyName("data_inicio")]
    public string? DateStart { get; set; }

    /// <summary>
    /// Field Name: data_fim
    /// </summary>
    [JsonPropertyName("data_fim")]
    public string? DateEnd { get; set; }

    /// <summary>
    /// Field Name: tipo_ato
    /// </summary>
    [JsonPropertyName("tipo_ato")]
    public string? ActType { get; set; }

    /// <summary>
    /// Field Name: numero_ato
    /// </summary>
    [JsonPropertyName("numero_ato")]
    public string? ActNumber { get; set; }

    /// <summary>
    /// Field Name: ano_ato
    /// </summary>
    [JsonPropertyName("ano_ato")]
    public string? ActYear { get; set; }
}
