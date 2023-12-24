namespace SimpleJobs.BrasilAPI;

public class FIPEVehicleTypeResponse
{
    [JsonPropertyName("nome")]
    public string? Nome { get; set; }

    [JsonPropertyName("valor")]
    public string? Valor { get; set; }
}