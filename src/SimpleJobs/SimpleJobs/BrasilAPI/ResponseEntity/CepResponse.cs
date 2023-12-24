namespace SimpleJobs.BrasilAPI;

public class CepResponse
{

    [JsonPropertyName("cep")]
    public string? ZipCode { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("neighborhood")]
    public string? Neighborhood { get; set; }

    [JsonPropertyName("street")]
    public string? Street { get; set; }

    [JsonPropertyName("service")]
    public string? Service { get; set; }

    [JsonPropertyName("location")]
    public CepLocation? Location { get; set; }

}

public class CepLocation
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("coordinates")]
    public CepCoordinates? Coordinates { get; set; }
}

public class CepCoordinates
{
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }

    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }
}


