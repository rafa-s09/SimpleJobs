namespace SimpleJobs.BrasilAPI;

public class FIPETableResponse
{
    [JsonPropertyName("codigo")]
    public int? Codigo { get; set; }

    [JsonPropertyName("mes")]
    public string? Mes { get; set; }
}
