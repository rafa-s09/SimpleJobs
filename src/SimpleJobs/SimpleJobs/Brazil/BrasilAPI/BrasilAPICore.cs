﻿namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// This class is an abstraction for accessing the <see href="https://brasilapi.com.br/">BrasilAPI</see> project.<br/>
/// Last Update (MM-dd-yyyy): 12-14-2022
/// </summary>
public static class BrasilAPICore
{
    #region BANKS   

    /// <summary>
    /// Search for bank information from a code
    /// </summary>
    /// <param name="code">Bank Code</param>
    /// <returns>Bank</returns>
    public static async Task<IResponseBase<Bank>> GetBank(string code)
    {
        if (string.IsNullOrEmpty(code.Replace(" ","")))
            return new ResponseBase<Bank>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "banks/v1/" + code;
        return await GetAsync<Bank>(url, error404: "Bank code not found.");
    }

    /// <summary>
    /// Returns information from all banks in Brazil
    /// </summary>
    /// <returns>List of Banks</returns>
    public static async Task<IResponseBase<IEnumerable<Bank>>> GetBanks()
    {
        string url = "banks/v1";
        return await GetAsync<IEnumerable<Bank>>(url);
    }

    #endregion BANKS

    #region CEP

    /// <summary>
    /// Search by zip code with multiple fallback providers. <br/>
    /// <i>Obsolete: This method is obsolete. Call GetCepV2 instead. </i>
    /// </summary>
    /// <param name="cep">Zip code</param>
    /// <returns>Full Adress</returns>
    [Obsolete("This method is obsolete. Call GetCepV2 instead.", false)]
    public static async Task<IResponseBase<Cep>> GetCep(string cep)
    {
        if (string.IsNullOrEmpty(cep.Replace(" ", "")))
            return new ResponseBase<Cep>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        if (cep.ClearSpecialCharacters().Length != 8)
            return new ResponseBase<Cep>() { Success = false, Mensage = "Invalid CEP length, expected 8 digits" };

        string url = "cep/v1/" + cep.ClearSpecialCharacters();
        return await GetAsync<Cep>(url, error404: "All CEP services returned an error.");
    }

    /// <summary>
    /// Search by zip code with multiple fallback providers. (Version: 2)
    /// </summary>
    /// <param name="cep">Zip code</param>
    /// <returns>Full Adress</returns>
    public static async Task<IResponseBase<Cep>> GetCepV2(string cep)
    {
        if (string.IsNullOrEmpty(cep.Replace(" ", "")))
            return new ResponseBase<Cep>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        if (cep.ClearSpecialCharacters().Length != 8)
            return new ResponseBase<Cep>() { Success = false, Mensage = "Invalid CEP length, expected 8 digits" };

        string url = "cep/v2/" + cep.ClearSpecialCharacters();
        return await GetAsync<Cep>(url, error404: "All CEP services returned an error.");
    }

    #endregion CEP

    #region CNPJ

    /// <summary>
    /// Search by CNPJ Information in the Minha Receita API.
    /// </summary>
    /// <param name="cnpj">CNPJ Document Number</param>
    /// <returns>CNPJ Information</returns>
    public static async Task<IResponseBase<Cnpj>> GetCNPJInfo(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj.Replace(" ", "")))
            return new ResponseBase<Cnpj>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        if (BrazilValidations.CheckForCNPJ(cnpj) != BrazilValidationResult.Success)
            return new ResponseBase<Cnpj>() { Success = false, Mensage = "Wrong CNPJ Number" };

        string url = "cnpj/v1/" + cnpj.ClearSpecialCharacters();
        return await GetAsync<Cnpj>(url, error404: "Error returned by Minha Receita API.");
    }

    #endregion CNPJ 

    #region DDD

    /// <summary>
    /// Returns state and list of cities by DDD
    /// </summary>
    /// <returns>State and list of cities by DDD</returns>
    public static async Task<IResponseBase<DDD>> GetDDD(int ddd)
    {
        string url = "ddd/v1/" + ddd;
        return await GetAsync<DDD>(url, error404: "DDD code not found.", error500: "All DDD services returned an error.");
    }

    #endregion DDD

    #region Brazil National holidays

    /// <summary>
    /// Lists national holidays for a given year. <br/>
    /// Calculates mobile holidays based on Easter and adds fixed holidays
    /// </summary>
    /// <param name="year">Year to calculate holidays.</param>
    /// <returns>Lists of Brazil national holidays</returns>
    public static async Task<IResponseBase<IEnumerable<BrazilHolidays>>> GetHolidays(int year)
    {
        string url = "feriados/v1/" + year;
        return await GetAsync<IEnumerable<BrazilHolidays>>(url, error404: "Year outside the supported range between 1900 and 2199.", error500: "Error calculating holidays.");
    }

    #endregion Brazil National holidays

    #region FIPE

    /// <summary>
    /// Lists vehicle brands by vehicle type.
    /// </summary>
    /// /// <param name="vehicleType">Vehicle Type Enumerator</param>
    /// <returns>List of the vehicle brands by vehicle type.</returns>
    public static async Task<IResponseBase<IEnumerable<FipeBrands>>> GetFIPEBrands(VehicleTypes vehicleType)
    {
        string url = "fipe/marcas/v1/" + vehicleType.GetVehicleType();
        return await GetAsync<IEnumerable<FipeBrands>>(url, "Invalid reference table.");
    }

    /// <summary>
    /// Consult the price of the vehicle according to the fipe table.
    /// </summary>
    /// /// <param name="fipeCode">FIPE Code</param>
    /// <returns>Price information of the vehicle according to the fipe table.</returns>
    public static async Task<IResponseBase<IEnumerable<FipeVehicle>>> GetFIPEPrice(string fipeCode)
    {
        if (string.IsNullOrEmpty(fipeCode.Replace(" ", "")))
            return new ResponseBase<IEnumerable<FipeVehicle>>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        if (string.IsNullOrEmpty(fipeCode.ClearSymbols()))
            return new ResponseBase<IEnumerable<FipeVehicle>>() { Success = false, Mensage = "Invalid FIPE code." };

        string url = "fipe/preco/v1/" + fipeCode.ClearSymbols();
        return await GetAsync<IEnumerable<FipeVehicle>>(url,  "Invalid FIPE code.", "ISBN not found");
    }

    /// <summary>
    /// Lists existing reference tables.
    /// </summary>
    /// <returns>List of the existing reference tables.</returns>
    public static async Task<IResponseBase<IEnumerable<FipeTables>>> GetFIPETable()
    {
        string url = "fipe/tabelas/v1";
        return await GetAsync<IEnumerable<FipeTables>>(url);
    }

    #endregion FIPE

    #region IBGE

    /// <summary>
    /// Returns the municipalities of the federative unit
    /// </summary>
    /// <param name="uf">Federative unit</param>
    /// <returns>List of IBGE Code Federative Unit</returns>
    public static async Task<IResponseBase<IEnumerable<IbgeCities>>> GetIBGECodes(string uf)
    {
        if (string.IsNullOrEmpty(uf.Replace(" ", "")))
            return new ResponseBase<IEnumerable<IbgeCities>>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        if (uf.Length != 2)
            return new ResponseBase<IEnumerable<IbgeCities>>() { Success = false, Mensage = "Invalid uf length, expected only 2 characters." };

        string url = "ibge/municipios/v1/" + uf.ToLower() + "?providers=dados-abertos-br,gov,wikipedia";
        return await GetAsync<IEnumerable<IbgeCities>>(url, error404: "UF not found");
    }

    /// <summary>
    /// Returns information from all states in Brazil
    /// </summary>
    /// <returns>List of information from all states in Brazil.</returns>
    public static async Task<IResponseBase<IEnumerable<Ibge>>> GetIBGE()
    {
        string url = "ibge/uf/v1";
        return await GetAsync<IEnumerable<Ibge>>(url);
    }

    /// <summary>
    /// Search the information of a state from the abbreviation or code
    /// </summary>
    /// <param name="ibgeCode">IBGE Code</param>
    /// <returns>Information of a state in Brazil.</returns>
    public static async Task<IResponseBase<Ibge>> GetIBGE(string ibgeCode)
    {
        if (string.IsNullOrEmpty(ibgeCode.Replace(" ", "")))
            return new ResponseBase<Ibge>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "ibge/uf/v1/" + ibgeCode;
        return await GetAsync<Ibge>(url, error404: "IBGE Code not found.");
    }

    #endregion IBGE

    #region ISBN

    /// <summary>
    /// Information about the book from the ISBN
    /// </summary>
    /// <param name="isbn">ISBN Code</param>
    /// <returns>Information about the book from the ISBN</returns>
    public static async Task<IResponseBase<Isbn>> GetISBNInfo(string isbn)
    {
        if (string.IsNullOrEmpty(isbn.Replace(" ", "")))
            return new ResponseBase<Isbn>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "isbn/v1/" + isbn;
        return await GetAsync<Isbn>(url, "Invalid ISBN", "ISBN not found", "All ISBN services returned an error.");
    }

    #endregion ISBN

    #region NCM

    /// <summary>
    /// Returns information from all NCMs
    /// </summary>
    /// <returns>List of information from all NCMs</returns>
    public static async Task<IResponseBase<IEnumerable<Ncm>>> GetNCM()
    {
        string url = "ncm/v1";
        return await GetAsync<IEnumerable<Ncm>>(url);
    }

    /// <summary>
    /// Search for NCMs based on a code or description.
    /// </summary>
    /// <param name="ncmCode">NCM Code</param>
    /// <returns>List of NCMs based on a code or description.</returns>
    public static async Task<IResponseBase<IEnumerable<Ncm>>> SearchNCM(string ncmCode)
    {
        if (string.IsNullOrEmpty(ncmCode.Replace(" ", "")))
            return new ResponseBase<IEnumerable<Ncm>> () { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "ncm/v1?search=" + ncmCode.ClearSymbols();
        return await GetAsync<IEnumerable<Ncm>>(url);
    }

    /// <summary>
    /// Search the information of an NCM from a code
    /// </summary>
    /// <param name="ncmCode">NCM Code</param>
    /// <returns>Information of an NCM.</returns>
    public static async Task<IResponseBase<Ncm>> GetNCM(string ncmCode)
    {
        if (string.IsNullOrEmpty(ncmCode.Replace(" ", "")))
            return new ResponseBase<Ncm>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "ncm/v1/" + ncmCode.ClearSymbols(); 
        return await GetAsync<Ncm>(url, error404: "NCM code not found.");
    }

    #endregion NCM

    #region REGISTRO BR

    /// <summary>
    /// Evaluates the status of a .br domain, according Registro BR
    /// </summary>
    /// <param name="domain">Domain URL</param>
    /// <returns>Status of a .br domain</returns>
    public static async Task<IResponseBase<DomainBr>> CheckDomainBR(string domain)
    {
        if (string.IsNullOrEmpty(domain.Replace(" ", "")))
            return new ResponseBase<DomainBr>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "registrobr/v1/" + domain;
        return await GetAsync<DomainBr>(url, "The domain was not entered correctly!");
    }

    #endregion REGISTRO BR

    #region Brazil Taxes

    /// <summary>
    /// Returns interest rates and some official Brazil indices
    /// </summary>
    /// <returns> </returns>
    public static async Task<IResponseBase<IEnumerable<BrazilTaxes>>> GetBrTaxes()
    {
        string url = "taxas/v1";
        return await GetAsync<IEnumerable<BrazilTaxes>>(url);
    }

    /// <summary>
    /// Search the information of a tax from its name/acronym
    /// </summary>
    /// <param name="taxName">Tax Name or Acronym</param>
    /// <returns>Tax Information</returns>
    public static async Task<IResponseBase<BrazilTaxes>> GetBrTax(string taxName)
    {
        if (string.IsNullOrEmpty(taxName.Replace(" ", "")))
            return new ResponseBase<BrazilTaxes>() { Success = false, Mensage = "The entered value cannot be null or empty." };

        string url = "taxas/v1/" + taxName;
        return await GetAsync<BrazilTaxes>(url, error404: "Tax or Index not found.");
    }

    #endregion Brazil Taxes

    #region PRIVATE    

    /// <summary>
    /// Send the request to BrasilAPI and get the response
    /// </summary>
    /// <typeparam name="TModel">Response model</typeparam>
    /// <param name="url">Url</param>
    /// <param name="error400">Custom Error 400 mensage</param>
    /// <param name="error404">Custom Error 404 mensage</param>
    /// <param name="error500">Custom Error 500 mensage</param>
    /// <returns>IResponseBase of the TModel</returns>
    private static async Task<IResponseBase<TModel>> GetAsync<TModel>(string url, string error400 = "", string error404 = "", string error500 = "") where TModel : class
    {
        try
        {
#pragma warning disable IDE0063 // Usar a instrução 'using' simples
            using (HttpClient client = new())
            {
                client.BaseAddress= new Uri("https://brasilapi.com.br/api/");
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    // Special Error Check 400
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        return new ResponseBase<TModel>() { Success = false, Mensage = string.IsNullOrEmpty(error400) ? "Not valid or inconsistent response." : "400 - " + error400 };

                    // Special Error Check 404
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return new ResponseBase<TModel>() { Success = false, Mensage = string.IsNullOrEmpty(error404) ? "Not valid or inconsistent response." : "404 - " + error404 };

                    // Special Error Check 500
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                        return new ResponseBase<TModel>() { Success = false, Mensage = string.IsNullOrEmpty(error500) ? "Not valid or inconsistent response." : "500 - " + error500 };

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return new ResponseBase<TModel>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<TModel>(responseBody) };
                }
            }
#pragma warning restore IDE0063 // Usar a instrução 'using' simples
        }
#if DEBUG
        catch (HttpRequestException ex)
        {
            return new ResponseBase<TModel>() { Success = false, Mensage = ex.ToString() };
        }
        catch(Exception ex)
        {
            return new ResponseBase<TModel>() { Success = false, Mensage = ex.ToString() };
        }
#else
        catch
        {
            return new ResponseBase<TModel>() { Success = false, Mensage = "Not valid or inconsistent response." };
        }
#endif

    }

    /// <summary>
    /// Get name of Vehicle Type
    /// </summary>
    /// <param name="vehicleType">Vehicle Type</param>
    /// <returns>Vehicle Type Name</returns>
    private static string GetVehicleType(this VehicleTypes vehicleType)
    {
        return vehicleType switch
        {
            VehicleTypes.Car => "carros",
            VehicleTypes.Truck => "caminhoes",
            VehicleTypes.Bike => "motos",
            _ => "carros"
        };
    }

    #endregion PRIVATE
}

