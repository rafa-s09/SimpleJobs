namespace SimpleJobs.BrasilAPI;

/// <summary>
/// Representa uma classe utilitária para realizar solicitações assíncronas à BrasilAPI. <br/>
/// Ultimo update: 24/12/2023
/// </summary>
public class BrasilApiRequest
{
    #region Private

    /// <summary>
    /// A URL base BrasilAPI (<a href="https://brasilapi.com.br/api/" />)
    /// </summary>
    const string baseUrl = "https://brasilapi.com.br/api/";

    /// <summary>
    /// Envia uma solicitação assíncrona GET para a URL especificada e desserializa o conteúdo da resposta em um objeto do tipo TEntity.
    /// </summary>
    /// <typeparam name="TEntity">O tipo do objeto para desserializar o conteúdo da resposta.</typeparam>
    /// <param name="url">A URL relativa para o endpoint da API.</param>
    /// <returns>Um <see cref="BrasilApiResponse{TEntity}"/> contendo o resultado da solicitação à API.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using, utilizado como separação logica")]
    private static async Task<BrasilApiResponse<TEntity>> GetAsync<TEntity>(string url) where TEntity : class
    {
        try
        {
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(baseUrl);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        return new BrasilApiResponse<TEntity>() { Success = false, Message = "Consulta sem suceso, com status code: " + response.StatusCode, Content = null };

                    response.EnsureSuccessStatusCode(); // Garante que nao ouve falhas na verificação anterior
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return new BrasilApiResponse<TEntity>() { Success = true, Message = "Ok", Content = JsonSerializer.Deserialize<TEntity>(responseBody) };
                }
            }

        }
#if DEBUG
        catch (HttpRequestException ex)
        {
            return new BrasilApiResponse<TEntity>() { Success = false, Message = ex.ToString() };
        }
        catch (Exception ex)
        {
            return new BrasilApiResponse<TEntity>() { Success = false, Message = ex.ToString() };
        }
#else
        catch
        {
            return new BrasilApiResponse<TEntity>() { Success = false, Message = "Ocorreu um erro durante a consulta na API, verifique se os dados estão corretos." };
        }
#endif
    }

    /// <summary>
    /// Obtém o tipo de veículo como uma string com base no enumeração VehicleTypes.
    /// </summary>
    /// <param name="type">O tipo de veículo representado pela enumeração VehicleTypes.</param>
    /// <returns>Uma string que representa o tipo de veículo.</returns>
    private static string GetVehicleType(VehicleTypes type)
    {
        return type switch
        {
            VehicleTypes.Car => "carros",
            VehicleTypes.Truck => "caminhoes",
            VehicleTypes.Bike => "motos",
            _ => "carros",
        };
    }

    #endregion Private

    #region Bank

    /// <summary>
    /// Retorna informações de todos os bancos do Brasil
    /// </summary>
    /// <returns>Lista com as informações de todos os bancos do Brasil.</returns>
    public static async Task<BrasilApiResponse<List<BankResponse>>> GetBanks()
    {
        return await GetAsync<List<BankResponse>>("banks/v1");
    }

    /// <summary>
    /// Busca as informações de um banco a partir de um código
    /// </summary>
    /// <param name="code">Código do banco.</param>
    /// <returns>As informações de um banco.</returns>
    public static async Task<BrasilApiResponse<BankResponse>> GetBank(int code)
    {
        return await GetAsync<BankResponse>($"banks/v1/{code}");
    }

    #endregion Bank

    #region CEP

    /// <summary>
    /// Obtém informações associadas a um CEP no Brasil.
    /// </summary>
    /// <param name="cep">O CEP (Código de Endereçamento Postal) a ser consultado.</param>
    /// <returns>Respostacom informações do CEP.</returns>
    [Obsolete("Este método está obsoleto. Chame GetCepV2 em seu lugar", false)]
    public static async Task<BrasilApiResponse<CepResponse>> GetCep(string cep)
    {
        if (string.IsNullOrEmpty(cep.Replace(" ", "")))
            return new BrasilApiResponse<CepResponse>() { Success = false, Message = "The entered value cannot be null or empty.", Content = null };

        if (cep.ClearSpecialCharacters().Length != 8)
            return new BrasilApiResponse<CepResponse>() { Success = false, Message = "Invalid CEP length, expected 8 digits", Content = null };

        return await GetAsync<CepResponse>($"cep/v1/{cep.ClearSpecialCharacters()}");
    }

    /// <summary>
    /// Obtém informações associadas a um CEP no Brasil (Versão 2).
    /// </summary>
    /// <param name="cep">O CEP (Código de Endereçamento Postal) a ser consultado.</param>
    /// <returns>Respostacom informações do CEP.</returns>
    public static async Task<BrasilApiResponse<CepResponse>> GetCepV2(string cep)
    {
        if (string.IsNullOrEmpty(cep.Replace(" ", "")))
            return new BrasilApiResponse<CepResponse>() { Success = false, Message = "The entered value cannot be null or empty.", Content = null };

        if (cep.ClearSpecialCharacters().Length != 8)
            return new BrasilApiResponse<CepResponse>() { Success = false, Message = "Invalid CEP length, expected 8 digits", Content = null };

        return await GetAsync<CepResponse>($"cep/v2/{cep.ClearSpecialCharacters()}");
    }

    #endregion CEP

    #region CNPJ

    /// <summary>
    /// Busca dados de empresas por CNPJ
    /// </summary>
    /// <param name="cnpj">número do CNPJ (Cadastro Nacional de Pessoa Jurídica)</param>
    /// <returns>Dados da empresa registrados com o CNPJ</returns>
    public static async Task<BrasilApiResponse<CnpjResponse>> GetCNPJ(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj.Replace(" ", "")))
            return new BrasilApiResponse<CnpjResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        if (DocumentValidation.CNPJIsValid(cnpj) != DocumentValidationResponse.Valid)
            return new BrasilApiResponse<CnpjResponse>() { Success = false, Message = "O número fornecido do CNPJ é inválido.", Content = null };

        return await GetAsync<CnpjResponse>($"cnpj/v1/{cnpj.ClearSpecialCharacters()}");
    }

    #endregion CNPJ

    #region Corretoras

    /// <summary>
    /// Retorna informações referentes a Corretoras ativas listadas na CVM
    /// </summary>
    /// <returns>Retorna as corretoras nos arquivos da CVM.</returns>
    public static async Task<BrasilApiResponse<List<CorretoraResponse>>> GetCorretoras()
    {
        return await GetAsync<List<CorretoraResponse>>("cvm/corretoras/v1");
    }

    /// <summary>
    /// Retorna informações referentes a Corretora ativa listada na CVM
    /// </summary>
    /// <param name="cnpj">número do CNPJ (Cadastro Nacional de Pessoa Jurídica)</param>
    /// <returns>Retorna as corretoras nos arquivos da CVM.</returns>
    public static async Task<BrasilApiResponse<CorretoraResponse>> GetCorretora(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj.Replace(" ", "")))
            return new BrasilApiResponse<CorretoraResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        if (DocumentValidation.CNPJIsValid(cnpj) != DocumentValidationResponse.Valid)
            return new BrasilApiResponse<CorretoraResponse>() { Success = false, Message = "O número fornecido do CNPJ é inválido.", Content = null };

        return await GetAsync<CorretoraResponse>($"cvm/corretoras/v1/{cnpj.ClearSpecialCharacters()}");
    }

    #endregion Corretoras

    #region CPTEC

    /// <summary>
    /// Abstração e normalização de dados provenientes da CPTEC. Fonte oficial: CPTEC/INPE <br/>
    /// Retorna listagem com todas as cidades junto a seus respectivos códigos presentes nos serviços da CPTEC. <br/>
    /// <b>Nota:</b>Leve em consideração que o WebService do CPTEC as vezes é instável, então se não encontrar uma determinada cidade na listagem completa, tente buscando por parte de seu nome no endpoint de busca.
    /// </summary>
    /// <returns>Retorna listagem com todas as cidades junto a seus respectivos códigos presentes nos serviços da CPTEC.</returns>
    public static async Task<BrasilApiResponse<List<CPTECCityResponse>>> GetCPTECCities()
    {
        return await GetAsync<List<CPTECCityResponse>>("cptec/v1/cidade");
    }

    /// <summary>
    /// Abstração e normalização de dados provenientes da CPTEC. Fonte oficial: CPTEC/INPE <br/>
    /// Retorna listagem com todas as cidades correspondentes ao termo pesquisado junto a seus respectivos códigos presentes nos serviços da CPTEC. O Código destas cidades será utilizado para os serviços de meteorologia e a ondas (previsão oceânica) fornecido pelo centro. <br/>
    /// </summary>
    /// <param name="icaoCode"> Nome ou parte do nome da cidade a ser buscada.</param>
    /// <returns>Retorna listagem com todas as cidades junto a seus respectivos códigos presentes nos serviços da CPTEC.</returns>
    public static async Task<BrasilApiResponse<List<CPTECCityResponse>>> GetCPTECCities(string cityName)
    {
        if (string.IsNullOrEmpty(cityName))
            return new BrasilApiResponse<List<CPTECCityResponse>>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<List<CPTECCityResponse>>($"cptec/v1/cidade/{cityName}");
    }

    /// <summary>
    /// Abstração e normalização de dados provenientes da CPTEC. Fonte oficial: CPTEC/INPE <br/>
    /// Retorna condições meteorológicas atuais nas capitais do país, com base nas estações de solo de seu aeroporto.
    /// </summary>
    /// <returns>Retorna condições meteorológicas atuais nas capitais do país.</returns>
    public static async Task<BrasilApiResponse<List<CPTECAirportResponse>>> GetCPTECCapital()
    {
        BrasilApiResponse<List<CPTECAirportResponse>> response = await GetAsync<List<CPTECAirportResponse>>("cptec/v1/clima/capital");
        return response;
    }

    /// <summary>
    /// Abstração e normalização de dados provenientes da CPTEC. Fonte oficial: CPTEC/INPE <br/>
    /// Retorna condições meteorológicas atuais no aeroporto solicitado. Este endpoint utiliza o código ICAO (4 dígitos) do aeroporto.
    /// </summary>
    /// <param name="icaoCode">Código ICAO (4 dígitos) do aeroporto.</param>
    /// <returns>Retorna condições meteorológicas atuais no aeroporto solicitado.</returns>
    public static async Task<BrasilApiResponse<CPTECAirportResponse>> GetCPTECAirport(string icaoCode)
    {
        if (string.IsNullOrEmpty(icaoCode))
            return new BrasilApiResponse<CPTECAirportResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        BrasilApiResponse<CPTECAirportResponse> response = await GetAsync<CPTECAirportResponse>($"cptec/v1/clima/aeroporto/{icaoCode}");
        return response;
    }

    /// <summary>
    /// Abstração e normalização de dados provenientes da CPTEC. Fonte oficial: CPTEC/INPE <br/>
    /// Retorna a previsão para a cidade informada para um período de 1 até 6 dias.
    /// </summary>
    /// <param name="cityCode">Código da cidade.</param>
    /// <param name="days">quantidade de dias da semana, deve ser de 1 a 6. (Opcional)</param>
    /// <returns>Retorna a previsão para a cidade informada.</returns>
    public static async Task<BrasilApiResponse<CPTECForecastResponse>> GetCPTECCityWeather(string cityCode, int days = -1)
    {
        if (days > 0 && days < 7)
            return await GetAsync<CPTECForecastResponse>($"cptec/v1/clima/previsao/{cityCode}/{days}");

        return await GetAsync<CPTECForecastResponse>($"cptec/v1/clima/previsao/{cityCode}");
    }

    /// <summary>
    /// Abstração e normalização de dados provenientes da CPTEC. Fonte oficial: CPTEC/INPE <br/>
    /// Retorna a previsão oceânica para a cidade informada para um período de 1 até 6 dias.
    /// </summary>
    /// <param name="cityCode">Código da cidade.</param>
    /// <param name="days">Quantidade de dias da semana, deve ser de 1 a 6. (Opcional)</param>
    /// <returns>Retorna a previsão oceânica para a cidade informada.</returns>
    public static async Task<BrasilApiResponse<CPTECOceanResponse>> GetCPTECOceanWeather(string cityCode, int days = -1)
    {
        if (days > 0 && days < 7)
            return await GetAsync<CPTECOceanResponse>($"cptec/v1/clima/previsao/{cityCode}/{days}");

        return await GetAsync<CPTECOceanResponse>($"cptec/v1/clima/previsao/{cityCode}");
    }

    #endregion CPTEC

    #region DDD

    /// <summary>
    /// Retorna informações relacionadas a DDDs (Discagem Direta à Distância).
    /// </summary>
    /// <param name="ddd">codigo DDD (Discagem Direta à Distância)</param>
    /// <returns>Retorna estado e lista de cidades por DDD (Discagem Direta à Distância).</returns>
    public static async Task<BrasilApiResponse<DDDResponse>> GetDDD(int ddd)
    {
        return await GetAsync<DDDResponse>($"ddd/v1/{ddd}");
    }

    #endregion DDD

    #region Feriados Nacionais

    /// <summary>
    /// Calcula os feriados móveis baseados na Páscoa e adiciona os feriados fixos.
    /// </summary>
    /// <param name="ano">Ano para calcular os feriados.</param>
    /// <returns>Retorna informações sobre feriados nacionais do ano informado.</returns>
    public static async Task<BrasilApiResponse<List<FeriadosNacionaisReponse>>> GetFeriadosNacionais(int ano)
    {
        return await GetAsync<List<FeriadosNacionaisReponse>>($"feriados/v1/{ano}");
    }

    #endregion Feriados Nacionais

    #region FIPE

    /// <summary>
    /// Informações sobre Preço Médio de Veículos fornecido pela FIPE (Fundação Instituto de Pesquisas Econômicas)
    /// </summary>
    /// <param name="vehicleType">Tipos de veículos FIPE.</param>
    /// <returns>Lista as marcas de veículos referente ao tipo de veículo.</returns>
    public static async Task<BrasilApiResponse<List<FIPEVehicleTypeResponse>>> GetFIPETipoVeiculo(VehicleTypes vehicleType)
    {
        return await GetAsync<List<FIPEVehicleTypeResponse>>($"fipe/marcas/v1/{GetVehicleType(vehicleType)}");
    }

    /// <summary>
    /// Consulta o preço do veículo segundo a tabela FIPE (Fundação Instituto de Pesquisas Econômicas).
    /// </summary>
    /// <param name="fipeCode">Código fipe do veículo.</param>
    /// <returns>Dados do veículo segundo a tabela FIPE (Fundação Instituto de Pesquisas Econômicas).</returns>
    public static async Task<BrasilApiResponse<List<FIPEVehicleResponse>>> GetFIPE(string fipeCode)
    {
        if (string.IsNullOrEmpty(fipeCode.Replace(" ", "")))
            return new BrasilApiResponse<List<FIPEVehicleResponse>>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<List<FIPEVehicleResponse>>($"fipe/preco/v1/{fipeCode.ClearSymbols()}");
    }

    /// <summary>
    /// Lista as tabelas de referência existentes.
    /// </summary>
    /// <returns>Lista das tabelas de referência.</returns>
    public static async Task<BrasilApiResponse<List<FIPETableResponse>>> GetFIPETables()
    {
        return await GetAsync<List<FIPETableResponse>>("fipe/tabelas/v1");
    }

    #endregion FIPE

    #region IBGE

    /// <summary>
    /// Retorna os municípios da unidade federativa
    /// </summary>
    /// <param name="uf">Unidade federativa</param>
    /// <returns>Lista de municípios</returns>
    public static async Task<BrasilApiResponse<List<IBGERegionsResponse>>> GetIBGERegions(string uf)
    {
        if (string.IsNullOrEmpty(uf.Replace(" ", "")))
            return new BrasilApiResponse<List<IBGERegionsResponse>>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        if (uf.Length != 2)
            return new BrasilApiResponse<List<IBGERegionsResponse>>() { Success = false, Message = "Comprimento do UF inválido, esperado apenas 2 caracteres.", Content = null };
        
        return await GetAsync<List<IBGERegionsResponse>>($"ibge/municipios/v1/{uf.ToLower()}?providers=dados-abertos-br,gov,wikipedia");
    }

    /// <summary>
    /// Retorna informações de todos estados do Brasil
    /// </summary>
    /// <returns>Lista de informações de todos estados do Brasil</returns>
    public static async Task<BrasilApiResponse<List<IBGEResponse>>> GetIBGE()
    {
        return await GetAsync<List<IBGEResponse>>("ibge/uf/v1");
    }

    /// <summary>
    /// Busca as informações de um estado a partir da sigla ou código
    /// </summary>
    /// <param name="ibgeCode">Sigla ou código IBGE</param>
    /// <returns>Informações da sigla registradas no IBGE</returns>
    public static async Task<BrasilApiResponse<IBGEResponse>> GetIBGE(string ibgeCode)
    {
        if (string.IsNullOrEmpty(ibgeCode.Replace(" ", "")))
            return new BrasilApiResponse<IBGEResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<IBGEResponse>($"ibge/uf/v1/{ibgeCode}");
    }

    #endregion IBGE

    #region ISBN

    /// <summary>
    /// Informações sobre livros publicados no Brasil (prefixo 65 ou 85) a partir do ISBN, um sistema internacional de identificação de livros que utiliza números para classificá-los por título, autor, país, editora e edição.
    /// </summary>
    /// <param name="isbn">Código INBS (O código informado pode conter traços (-) e ambos os formatos são aceitos, sendo eles o obsoleto de 10 dígitos e o atual de 13 dígitos).</param>
    /// <returns>Informações sobre o livro conforme o ISBN</returns>
    public static async Task<BrasilApiResponse<ISBNResponse>> GetISBN(string isbn)
    {
        if (string.IsNullOrEmpty(isbn.Replace(" ", "")))
            return new BrasilApiResponse<ISBNResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<ISBNResponse>($"isbn/v1/{isbn}");
    }

    #endregion ISBN

    #region NCM

    /// <summary>
    /// Lista informações de todos os NCMs
    /// </summary>
    /// <returns>Retorna informações de todos os NCMs</returns>
    public static async Task<BrasilApiResponse<List<NCMResponse>>> GetNCM()
    {
        return await GetAsync<List<NCMResponse>>("ncm/v1");
    }

    /// <summary>
    /// Pesquisa por NCMs a partir de um código ou descrição.
    /// </summary>
    /// <param name="ncm">Código NCM</param>
    /// <returns>Retorna informações dos NCMs</returns>
    public static async Task<BrasilApiResponse<List<NCMResponse>>> SearchNCM(string ncm)
    {
        if (string.IsNullOrEmpty(ncm.Replace(" ", "")))
            return new BrasilApiResponse<List<NCMResponse>>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<List<NCMResponse>>($"ncm/v1?search={ncm.ClearSymbols()}");
    }

    /// <summary>
    /// Busca as informações de um NCM a partir de um código
    /// </summary>
    /// <param name="ncm">Código NCM</param>
    /// <returns>Retorna informações do NCM</returns>
    public static async Task<BrasilApiResponse<NCMResponse>> GetNCM(string ncm)
    {
        if (string.IsNullOrEmpty(ncm.Replace(" ", "")))
            return new BrasilApiResponse<NCMResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<NCMResponse>($"ncm/v1/{ncm.ClearSymbols()}");
    }

    #endregion NCM

    #region PIX

    /// <summary>
    /// Informações referentes ao PIX.
    /// </summary>
    /// <returns>Retorna informações de todos os participantes do PIX no dia atual ou anterior.</returns>
    public static async Task<BrasilApiResponse<List<PIXResponse>>> GetPIX()
    {
        return await GetAsync<List<PIXResponse>>("pix/v1/participants");
    }

    #endregion PIX

    #region REGISTRO BR

    /// <summary>
    /// Avalia o status de um dominio no registro.br
    /// </summary>
    /// <param name="domain">Dominio</param>
    /// <returns>Status do dominio</returns>
    public static async Task<BrasilApiResponse<RegistroBrResponse>> CheckRegistroBr(string domain)
    {
        if (string.IsNullOrEmpty(domain.Replace(" ", "")))
            return new BrasilApiResponse<RegistroBrResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<RegistroBrResponse>($"registrobr/v1/{domain}");
    }

    #endregion REGISTRO BR

    #region TAXAS

    /// <summary>
    /// Taxas de juros e índices oficiais
    /// </summary>
    /// <returns>Retorna as taxas de juros e alguns índices oficiais do Brasil.</returns>
    public static async Task<BrasilApiResponse<List<TaxasResponse>>> GetTaxes()
    {
        return await GetAsync<List<TaxasResponse>>("taxas/v1");
    }

    /// <summary>
    /// Busca as informações de uma taxa a partir do seu nome/sigla
    /// </summary>
    /// <param name="taxName">Nome/sigla da taxa</param>
    /// <returns>Retorna a taxa de juros de índices oficiais do Brasil.</returns>
    public static async Task<BrasilApiResponse<TaxasResponse>> GetTaxes(string taxName)
    {
        if (string.IsNullOrEmpty(taxName.Replace(" ", "")))
            return new BrasilApiResponse<TaxasResponse>() { Success = false, Message = "O valor inserido não pode ser nulo ou vazio.", Content = null };

        return await GetAsync<TaxasResponse>($"taxas/v1/{taxName}");
    }

    #endregion TAXAS
}

