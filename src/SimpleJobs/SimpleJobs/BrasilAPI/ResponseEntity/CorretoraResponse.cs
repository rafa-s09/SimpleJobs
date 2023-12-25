namespace SimpleJobs.BrasilAPI;

public class CorretoraResponse
{
    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }

    [JsonPropertyName("cep")]
    public string? CEP { get; set; }

    [JsonPropertyName("cnpj")]
    public string? CNPJ { get; set; }

    [JsonPropertyName("codigo_cvm")]
    public string? CVM { get; set; }

    [JsonPropertyName("complemento")]
    public string? Complemento { get; set; }

    [JsonPropertyName("data_inicio_situacao")]
    public string? DtInicioSituacao { get; set; }

    [JsonPropertyName("data_patrimonio_liquido")]
    public string? DtPatrimonioLiquido { get; set; }

    [JsonPropertyName("data_registro")]
    public string? DtRegistro { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("logradouro")]
    public string? Logradouro { get; set; }

    [JsonPropertyName("municipio")]
    public string? Municipio { get; set; }

    [JsonPropertyName("nome_social")]
    public string? NomeSocial { get; set; }

    [JsonPropertyName("nome_comercial")]
    public string? NomeComercial { get; set; }

    [JsonPropertyName("pais")]
    public string? Pais { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("telefone")]
    public string? Telefone { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("uf")]
    public string? UF { get; set; }

    [JsonPropertyName("valor_patrimonio_liquido")]
    public string? PatrimonioLiquido { get; set; }

}
