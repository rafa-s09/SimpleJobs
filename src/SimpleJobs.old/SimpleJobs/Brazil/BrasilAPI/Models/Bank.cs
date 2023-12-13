namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Banking system information
/// </summary>
public class Bank
{
    /// <summary>
    /// Field Name: ispb
    /// </summary>
    [JsonPropertyName("ispb")]
    public string? ISPB { get; set; }

    /// <summary>
    /// Field Name: name
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Field Name: code
    /// </summary>
    [JsonPropertyName("code")]
    public int? Code { get; set; }

    /// <summary>
    /// Field Name: fullName
    /// </summary>
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

}
