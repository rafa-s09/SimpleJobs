namespace SimpleJobs.BrasilAPI;

public class FeriadosNacionaisReponse
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("fullName"), JsonIgnore]
    public string? FullName { get; set; }
}
