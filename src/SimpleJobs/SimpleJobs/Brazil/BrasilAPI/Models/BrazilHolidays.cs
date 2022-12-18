using System.Text.Json.Serialization;

namespace SimpleJobs.Brazil.BrasilAPI;

public class BrazilHolidays
{
    /// <summary>
    /// Field Name: date
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>
    /// Field Name: type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Field Name: name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Field Name: fullName
    /// </summary>
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

}
