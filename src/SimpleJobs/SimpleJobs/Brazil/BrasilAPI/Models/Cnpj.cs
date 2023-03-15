using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Company data by CNPJ
/// </summary>
public class Cnpj
{
    /// <summary>
    /// Field Name: cnpj
    /// </summary>
    [JsonPropertyName("cnpj")]
    public string? CnpjNumber { get; set; }

    /// <summary>
    /// Field Name: identificador_matriz_filial
    /// </summary>
    [JsonPropertyName("identificador_matriz_filial")]
    public int? BranchMatrixIdentifier { get; set; }

    /// <summary>
    /// Field Name: descricao_matriz_filial
    /// </summary>
    [JsonPropertyName("descricao_matriz_filial")]
    public string? BranchOfficeDescription { get; set; }

    /// <summary>
    /// Field Name: razao_social
    /// </summary>
    [JsonPropertyName("razao_social")]
    public string? CorporateName { get; set; }

    /// <summary>
    /// Field Name: nome_fantasia
    /// </summary>
    [JsonPropertyName("nome_fantasia")]
    public string? FantasyName { get; set; }

    /// <summary>
    /// Field Name: situacao_cadastral
    /// </summary>
    [JsonPropertyName("situacao_cadastral")]
    public int? RegistrationStatus { get; set; }

    /// <summary>
    /// Field Name: descricao_situacao_cadastral
    /// </summary>
    [JsonPropertyName("descricao_situacao_cadastral")]
    public string? DescriptionOfRegistrationStatus { get; set; }

    /// <summary>
    /// Field Name: data_situacao_cadastral
    /// </summary>
    [JsonPropertyName("data_situacao_cadastral")]
    public string? RegistrationStatusDate { get; set; }

    /// <summary>
    /// Field Name: motivo_situacao_cadastral
    /// </summary>
    [JsonPropertyName("motivo_situacao_cadastral")]
    public int? ReasonForRegistrationStatus { get; set; }

    /// <summary>
    /// Field Name: nome_cidade_exterior
    /// </summary>
    [JsonPropertyName("nome_cidade_exterior")]
    public string? OuterCityName { get; set; }

    /// <summary>
    /// Field Name: codigo_natureza_juridica
    /// </summary>
    [JsonPropertyName("codigo_natureza_juridica")]
    public int? CodeLegalNature { get; set; }

    /// <summary>
    /// Field Name: data_inicio_atividade
    /// </summary>
    [JsonPropertyName("data_inicio_atividade")]
    public string? ActivityStartDate { get; set; }

    /// <summary>
    /// Field Name: cnae_fiscal
    /// </summary>
    [JsonPropertyName("cnae_fiscal")]
    public int? FiscalCnae { get; set; }

    /// <summary>
    /// Field Name: cnae_fiscal_descricao
    /// </summary>
    [JsonPropertyName("cnae_fiscal_descricao")]
    public string? CnaeFiscalDescription { get; set; }

    /// <summary>
    /// Field Name: descricao_tipo_logradouro
    /// </summary>
    [JsonPropertyName("descricao_tipo_logradouro")]
    public string? StreetTypeDescription { get; set; }

    /// <summary>
    /// Field Name: logradouro
    /// </summary>
    [JsonPropertyName("logradouro")]
    public string? Street { get; set; }

    /// <summary>
    /// Field Name: numero
    /// </summary>
    [JsonPropertyName("numero")]
    public string? Number { get; set; }

    /// <summary>
    /// Field Name: complemento
    /// </summary>
    [JsonPropertyName("complemento")]
    public string? Complement { get; set; }

    /// <summary>
    /// Field Name: bairro
    /// </summary>
    [JsonPropertyName("bairro")]
    public string? Neighborhood { get; set; }

    /// <summary>
    /// Field Name: cep
    /// </summary>
    [JsonPropertyName("cep")]
    public string? ZipCode { get; set; }

    /// <summary>
    /// Field Name: uf
    /// </summary>
    [JsonPropertyName("uf")]
    public string? UF { get; set; }

    /// <summary>
    /// Field Name: codigo_municipio
    /// </summary>
    [JsonPropertyName("codigo_municipio")]
    public int? CityCode { get; set; }

    /// <summary>
    /// Field Name: municipio
    /// </summary>
    [JsonPropertyName("municipio")]
    public string? City { get; set; }

    /// <summary>
    /// Field Name: ddd_telefone_1
    /// </summary>
    [JsonPropertyName("ddd_telefone_1")]
    public string? PhoneDDD1 { get; set; }

    /// <summary>
    /// Field Name: ddd_telefone_2
    /// </summary>
    [JsonPropertyName("ddd_telefone_2")]
    public string? PhoneDDD2 { get; set; }

    /// <summary>
    /// Field Name: ddd_fax
    /// </summary>
    [JsonPropertyName("ddd_fax")]
    public string? FaxDDD { get; set; }

    /// <summary>
    /// Field Name: qualificacao_do_responsavel
    /// </summary>
    [JsonPropertyName("qualificacao_do_responsavel")]
    public int? QualificationOfTheResponsible { get; set; }

    /// <summary>
    /// Field Name: capital_social
    /// </summary>
    [JsonPropertyName("capital_social")]
    public long? ShareCapital { get; set; }

    /// <summary>
    /// Field Name: porte
    /// </summary>
    [JsonPropertyName("porte")]
    public string? Size { get; set; }

    /// <summary>
    /// Field Name: descricao_porte
    /// </summary>
    [JsonPropertyName("descricao_porte")]
    public string? SizeDescription { get; set; }

    /// <summary>
    /// Field Name: opcao_pelo_simples
    /// </summary>
    [JsonPropertyName("opcao_pelo_simples")]
    public bool? OptingForTheSimple { get; set; }

    /// <summary>
    /// Field Name: data_opcao_pelo_simples
    /// </summary>
    [JsonPropertyName("data_opcao_pelo_simples")]
    public string? DateOptionForSimple { get; set; }

    /// <summary>
    /// Field Name: data_exclusao_do_simples
    /// </summary>
    [JsonPropertyName("data_exclusao_do_simples")]
    public string? ExclusionDateForSimple { get; set; }

    /// <summary>
    /// Field Name: opcao_pelo_mei
    /// </summary>
    [JsonPropertyName("opcao_pelo_mei")]
    public bool? OptingForMei { get; set; }

    /// <summary>
    /// Field Name: situacao_especial
    /// </summary>
    [JsonPropertyName("situacao_especial")]
    public string? SpecialSituation { get; set; }

    /// <summary>
    /// Field Name: data_situacao_especial
    /// </summary>
    [JsonPropertyName("data_situacao_especial")]
    public string? DateSpecialSituation { get; set; }

    /// <summary>
    /// Field Name: cnaes_secundarios
    /// </summary>
    [JsonPropertyName("cnaes_secundarios")]
    public List<Cnaes>? SecundaryCnaes { get; set; }

    /// <summary>
    /// Field Name: qsa
    /// </summary>
    [JsonPropertyName("qsa")]
    public List<Qsa>? Qsa { get; set; }

}

/// <summary>
/// Company Cnaes
/// </summary>
public class Cnaes
{
    /// <summary>
    /// Field Name: codigo
    /// </summary>
    [JsonPropertyName("codigo")]
    public int? Code { get; set; }

    /// <summary>
    /// Field Name: descricao
    /// </summary>
    [JsonPropertyName("descricao")]
    public string? Description { get; set; }
}

/// <summary>
/// Company Qsa
/// </summary>
public class Qsa
{

    /// <summary>
    /// Field Name: identificador_de_socio
    /// </summary>
    [JsonPropertyName("identificador_de_socio")]
    public int? BusinessPartnerIdentifier { get; set; }

    /// <summary>
    /// Field Name: nome_socio
    /// </summary>
    [JsonPropertyName("nome_socio")]
    public string? BusinessPartnerName { get; set; }

    /// <summary>
    /// Field Name: cnpj_cpf_do_socio
    /// </summary>
    [JsonPropertyName("cnpj_cpf_do_socio")]
    public string? BusinessPartnerDocumentNumber { get; set; }

    /// <summary>
    /// Field Name: codigo_qualificacao_socio
    /// </summary>
    [JsonPropertyName("codigo_qualificacao_socio")]
    public int? PartnerQualificationCode { get; set; }

    /// <summary>
    /// Field Name: percentual_capital_social
    /// </summary>
    [JsonPropertyName("percentual_capital_social")]
    public int? ShareCapitalPercentage { get; set; }

    /// <summary>
    /// Field Name: data_entrada_sociedade
    /// </summary>
    [JsonPropertyName("data_entrada_sociedade")]
    public string? CompanyEntryDate { get; set; }

    /// <summary>
    /// Field Name: cpf_representante_legal
    /// </summary>
    [JsonPropertyName("cpf_representante_legal")]
    public string? CpfLegalRepresentative { get; set; }

    /// <summary>
    /// Field Name: nome_representante_legal
    /// </summary>
    [JsonPropertyName("nome_representante_legal")]
    public string? LegalRepresentativeName { get; set; }

    /// <summary>
    /// Field Name: codigo_qualificacao_representante_legal
    /// </summary>
    [JsonPropertyName("codigo_qualificacao_representante_legal")]
    public int? LegalRepresentativeQualificationCode { get; set; }
}
