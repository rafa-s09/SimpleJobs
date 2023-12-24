namespace SimpleJobs.BrasilAPI;

public class NCMResponse
{
    [JsonPropertyName("codigo")]
    public string? Code { get; set; }

    [JsonPropertyName("descricao")]
    public string? Description { get; set; }

    [JsonPropertyName("data_inicio")]
    public string? DateStart { get; set; }

    [JsonPropertyName("data_fim")]
    public string? DateEnd { get; set; }

    [JsonPropertyName("tipo_ato")]
    public string? ActType { get; set; }

    [JsonPropertyName("numero_ato")]
    public string? ActNumber { get; set; }

    [JsonPropertyName("ano_ato")]
    public string? ActYear { get; set; }
}