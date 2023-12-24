namespace SimpleJobs.BrasilAPI;

public class CnpjResponse
{
    [JsonPropertyName("cnpj")]
    public string? CnpjNumber { get; set; }

    [JsonPropertyName("identificador_matriz_filial")]
    public int? BranchMatrixIdentifier { get; set; }

    [JsonPropertyName("descricao_matriz_filial")]
    public string? BranchOfficeDescription { get; set; }

    [JsonPropertyName("razao_social")]
    public string? CorporateName { get; set; }

    [JsonPropertyName("nome_fantasia")]
    public string? FantasyName { get; set; }

    [JsonPropertyName("situacao_cadastral")]
    public int? RegistrationStatus { get; set; }

    [JsonPropertyName("descricao_situacao_cadastral")]
    public string? DescriptionOfRegistrationStatus { get; set; }

    [JsonPropertyName("data_situacao_cadastral")]
    public string? RegistrationStatusDate { get; set; }

    [JsonPropertyName("motivo_situacao_cadastral")]
    public int? ReasonForRegistrationStatus { get; set; }

    [JsonPropertyName("nome_cidade_exterior")]
    public string? OuterCityName { get; set; }

    [JsonPropertyName("codigo_natureza_juridica")]
    public int? CodeLegalNature { get; set; }

    [JsonPropertyName("data_inicio_atividade")]
    public string? ActivityStartDate { get; set; }

    [JsonPropertyName("cnae_fiscal")]
    public int? FiscalCnae { get; set; }

    [JsonPropertyName("cnae_fiscal_descricao")]
    public string? CnaeFiscalDescription { get; set; }

    [JsonPropertyName("descricao_tipo_logradouro")]
    public string? StreetTypeDescription { get; set; }

    [JsonPropertyName("logradouro")]
    public string? Street { get; set; }

    [JsonPropertyName("numero")]
    public string? Number { get; set; }

    [JsonPropertyName("complemento")]
    public string? Complement { get; set; }

    [JsonPropertyName("bairro")]
    public string? Neighborhood { get; set; }

    [JsonPropertyName("cep")]
    public string? ZipCode { get; set; }

    [JsonPropertyName("uf")]
    public string? UF { get; set; }

    [JsonPropertyName("codigo_municipio")]
    public int? CityCode { get; set; }

    [JsonPropertyName("municipio")]
    public string? City { get; set; }

    [JsonPropertyName("ddd_telefone_1")]
    public string? PhoneDDD1 { get; set; }

    [JsonPropertyName("ddd_telefone_2")]
    public string? PhoneDDD2 { get; set; }

    [JsonPropertyName("ddd_fax")]
    public string? FaxDDD { get; set; }

    [JsonPropertyName("qualificacao_do_responsavel")]
    public int? QualificationOfTheResponsible { get; set; }

    [JsonPropertyName("capital_social")]
    public long? ShareCapital { get; set; }

    [JsonPropertyName("porte")]
    public string? Size { get; set; }

    [JsonPropertyName("descricao_porte")]
    public string? SizeDescription { get; set; }

    [JsonPropertyName("opcao_pelo_simples")]
    public bool? OptingForTheSimple { get; set; }

    [JsonPropertyName("data_opcao_pelo_simples")]
    public string? DateOptionForSimple { get; set; }

    [JsonPropertyName("data_exclusao_do_simples")]
    public string? ExclusionDateForSimple { get; set; }

    [JsonPropertyName("opcao_pelo_mei")]
    public bool? OptingForMei { get; set; }

    [JsonPropertyName("situacao_especial")]
    public string? SpecialSituation { get; set; }

    [JsonPropertyName("data_situacao_especial")]
    public string? DateSpecialSituation { get; set; }

    [JsonPropertyName("cnaes_secundarios")]
    public List<Cnaes>? SecundaryCnaes { get; set; }

    [JsonPropertyName("qsa")]
    public List<Qsa>? Qsa { get; set; }

}

public class Cnaes
{
    [JsonPropertyName("codigo")]
    public int? Code { get; set; }

    [JsonPropertyName("descricao")]
    public string? Description { get; set; }
}

public class Qsa
{
    [JsonPropertyName("identificador_de_socio")]
    public int? BusinessPartnerIdentifier { get; set; }

    [JsonPropertyName("nome_socio")]
    public string? BusinessPartnerName { get; set; }

    [JsonPropertyName("cnpj_cpf_do_socio")]
    public string? BusinessPartnerDocumentNumber { get; set; }

    [JsonPropertyName("codigo_qualificacao_socio")]
    public int? PartnerQualificationCode { get; set; }

    [JsonPropertyName("percentual_capital_social")]
    public int? ShareCapitalPercentage { get; set; }

    [JsonPropertyName("data_entrada_sociedade")]
    public string? CompanyEntryDate { get; set; }

    [JsonPropertyName("cpf_representante_legal")]
    public string? CpfLegalRepresentative { get; set; }

    [JsonPropertyName("nome_representante_legal")]
    public string? LegalRepresentativeName { get; set; }

    [JsonPropertyName("codigo_qualificacao_representante_legal")]
    public int? LegalRepresentativeQualificationCode { get; set; }
}
