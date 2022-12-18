namespace SimpleJobs.Brazil.BrasilAPI;

public class Cep
{
    /// <summary>
    /// Field Name: cep
    /// </summary>
    [JsonPropertyName("cep")]
    public string? ZipCode { get; set; }

    /// <summary>
    /// Field Name: state
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Field Name: city
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; } 

    /// <summary>
    /// Field Name: neighborhood
    /// </summary>
    [JsonPropertyName("neighborhood")]
    public string? Neighborhood { get; set; } 

    /// <summary>
    /// Field Name: street
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; set; } 

    /// <summary>
    /// Field Name: service
    /// </summary>
    [JsonPropertyName("service")]
    public string? Service { get; set; } 

    /// <summary>
    /// Field Name: location
    /// </summary>
    [JsonPropertyName("location")]
    public CepLocation? Location { get; set; }
}

public class CepLocation
{
    /// <summary>
    /// Field Name: type
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Field Name: coordinates
    /// </summary>
    [JsonPropertyName("coordinates")]
    public CepCoordinates? Coordinates { get; set; }
}

public class CepCoordinates
{
    /// <summary>
    /// Field Name: longitude
    /// </summary>
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }

    /// <summary>
    /// Field Name: latitude
    /// </summary>
    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }
}
