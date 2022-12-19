﻿namespace SimpleJobs.Brazil.BrasilAPI;

public class Ibge
{
    /// <summary>
    /// Field Name: id
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Field Name: sigla
    /// </summary>
    [JsonPropertyName("sigla")]
    public string? Acronym { get; set; }

    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    public IbgeRegions? Regions { get; set; }
}

public class IbgeRegions
{
    /// <summary>
    /// Field Name: id
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Field Name: sigla
    /// </summary>
    [JsonPropertyName("sigla")]
    public string? Acronym { get; set; }

    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }
}

public class IbgeCities
{
    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    /// <summary>
    /// Field Name: codigo_ibge
    /// </summary>
    [JsonPropertyName("codigo_ibge")]
    public string? IbgeCode { get; set; }
}
