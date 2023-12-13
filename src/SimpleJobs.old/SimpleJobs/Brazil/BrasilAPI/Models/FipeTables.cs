namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Information on Average Vehicle Price provided by FIPE (Fundação Instituto de Pesquisas Econômicas)
/// </summary>
public class FipeTables
{
    /// <summary>
    /// Field Name: codigo
    /// </summary>
    [JsonPropertyName("codigo")]
    public int? Code { get; set; }

    /// <summary>
    /// Field Name: mes
    /// </summary>
    [JsonPropertyName("mes")]
    public string? MonthYear { get; set; }
}

/// <summary>
/// Information on Average Vehicle Price provided by FIPE (Fundação Instituto de Pesquisas Econômicas)
/// </summary>
public class FipeBrands
{
    /// <summary>
    /// Field Name: nome
    /// </summary>
    [JsonPropertyName("nome")]
    public string? Name { get; set; }

    /// <summary>
    /// Field Name: valor
    /// </summary>
    [JsonPropertyName("valor")]
    public string? Value { get; set; }
}

/// <summary>
/// Information on Average Vehicle Price provided by FIPE (Fundação Instituto de Pesquisas Econômicas)
/// </summary>
public class FipeVehicle
{
    /// <summary>
    /// Field Name: valor
    /// </summary>
    [JsonPropertyName("valor")]
    public string? Value { get; set; }

    /// <summary>
    /// Field Name: marca
    /// </summary>
    [JsonPropertyName("marca")]
    public string? Brand { get; set; }

    /// <summary>
    /// Field Name: modelo
    /// </summary>
    [JsonPropertyName("modelo")]
    public string? Model { get; set; }

    /// <summary>
    /// Field Name: anoModelo
    /// </summary>
    [JsonPropertyName("anoModelo")]
    public int? ModelYear { get; set; }

    /// <summary>
    /// Field Name: combustivel
    /// </summary>
    [JsonPropertyName("combustivel")]
    public string? FuelType { get; set; }

    /// <summary>
    /// Field Name: codigoFipe
    /// </summary>
    [JsonPropertyName("codigoFipe")]
    public string? FipeCode { get; set; }

    /// <summary>
    /// Field Name: mesReferencia
    /// </summary>
    [JsonPropertyName("mesReferencia")]
    public string? MonthReference { get; set; }

    /// <summary>
    /// Field Name: tipoVeiculo
    /// </summary>
    [JsonPropertyName("tipoVeiculo")]
    public int? VehicleType { get; set; }

    /// <summary>
    /// Field Name: siglaCombustivel
    /// </summary>
    [JsonPropertyName("siglaCombustivel")]
    public string? FuelAcronym { get; set; }

    /// <summary>
    /// Field Name: dataConsulta
    /// </summary>
    [JsonPropertyName("dataConsulta")]
    public string? RequestDate { get; set; }
}
