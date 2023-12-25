namespace SimpleJobs.BrasilAPI;

public record BankResponse
{
    [JsonPropertyName("ispb")]
    public string? ISPB { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public int? Code { get; set; }

    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }
}
