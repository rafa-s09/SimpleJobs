using System.Runtime.ConstrainedExecution;

namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// This class is an abstraction for accessing the <see href="https://brasilapi.com.br/">BrasilAPI</see> project
/// </summary>
public class BrasilAPICore
{
    /// <summary>
    /// base Url for solicitation
    /// </summary>
    private readonly string _baseUrl = "https://brasilapi.com.br/api/";

    /// <summary>
    /// Http Client for comunication
    /// </summary>
    private static readonly HttpClient client = new();

    #region BANKS   

    /// <summary>
    /// Search for bank information from a code
    /// </summary>
    /// <param name="code">Bank Code</param>
    /// <returns>Bank</returns>
    public async Task<IResponseBase<Bank>> GetBank(string code)
    {
        string url = _baseUrl + "banks/v1/" + code;
        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            // Special Error Check
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new ResponseBase<Bank>() { Success = false, Mensage = "404 - Bank code not found." };

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<Bank>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<Bank>(responseBody) };
        }
        catch (HttpRequestException ex)
        {           

#if DEBUG
            return new ResponseBase<Bank>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<Bank>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
    }

    /// <summary>
    /// Returns information from all banks in Brazil
    /// </summary>
    /// <returns>List of Banks</returns>
    public async Task<IResponseBase<IList<Bank>>> GetBanks()
    {
        string url = _baseUrl + "banks/v1";

        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<IList<Bank>>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<List<Bank>>(responseBody) };
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            return new ResponseBase<IList<Bank>>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<IList<Bank>>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
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
    public async Task<IResponseBase<Cep>> GetCep(string cep)
    {
        string url = _baseUrl + "cep/v1/" + cep.ClearSpecialCharacters();
        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            // Special Error Check
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new ResponseBase<Cep>() { Success = false, Mensage = "404 - All CEP services returned an error." };

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<Cep>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<Cep>(responseBody) };
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            return new ResponseBase<Cep>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<Cep>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
    }

    /// <summary>
    /// Search by zip code with multiple fallback providers. (Version: 2)
    /// </summary>
    /// <param name="cep">Zip code</param>
    /// <returns>Full Adress</returns>
    public async Task<IResponseBase<Cep>> GetCepV2(string cep)
    {
        string url = _baseUrl + "cep/v2/" + cep.ClearSpecialCharacters();
        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            // Special Error Check
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new ResponseBase<Cep>() { Success = false, Mensage = "404 - All CEP services returned an error." };

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<Cep>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<Cep>(responseBody) };
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            return new ResponseBase<Cep>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<Cep>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
    }

    #endregion CEP

    #region CNPJ

    /// <summary>
    /// Search by CNPJ Information in the Minha Receita API.
    /// </summary>
    /// <param name="cnpj">CNPJ Document Number</param>
    /// <returns>CNPJ Information</returns>
    public async Task<IResponseBase<Cnpj>> GetCNPJInfo(string cnpj)
    {
        string url = _baseUrl + "cnpj/v1/" + cnpj.ClearSpecialCharacters();
        try
        {
            if(BrazilValidations.CheckForCNPJ(cnpj) != BrazilValidationResult.Success)
                return new ResponseBase<Cnpj>() { Success = false, Mensage = "Wrong CNPJ Number" };

            using HttpResponseMessage response = await client.GetAsync(url);

            // Special Error Check
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new ResponseBase<Cnpj>() { Success = false, Mensage = "404 - Error returned by Minha Receita API." };

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<Cnpj>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<Cnpj>(responseBody) };
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            return new ResponseBase<Cnpj>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<Cnpj>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
    }

    #endregion CNPJ 

    #region DDD

    /// <summary>
    /// Returns state and list of cities by DDD
    /// </summary>
    /// <returns>State and list of cities by DDD</returns>
    public async Task<IResponseBase<DDD>> GetDDD(int ddd)
    {
        string url = _baseUrl + "ddd/v1/" + ddd;
        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            // Special Error Check
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new ResponseBase<DDD>() { Success = false, Mensage = "404 - DDDcode not found." };

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return new ResponseBase<DDD>() { Success = false, Mensage = "500 - All DDD services returned an error." };

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<DDD>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<DDD>(responseBody) };
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            return new ResponseBase<DDD>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<DDD>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
    }

    #endregion DDD

    #region Brazil National holidays

    /// <summary>
    /// Lists national holidays for a given year. <br/>
    /// Calculates mobile holidays based on Easter and adds fixed holidays
    /// </summary>
    /// <param name="year">Year to calculate holidays.</param>
    /// <returns>Lists of Brazil national holidays</returns>
    public async Task<IResponseBase<BrazilHolidays>> GetHolidays(int year)
    {
        string url = _baseUrl + "feriados/v1/" + year;
        try
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            // Special Error Check
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new ResponseBase<BrazilHolidays>() { Success = false, Mensage = "404 - Year outside the supported range between 1900 and 2199." };

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return new ResponseBase<BrazilHolidays>() { Success = false, Mensage = "500 - Error calculating holidays." };

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return new ResponseBase<BrazilHolidays>() { Success = true, Mensage = "Ok", Content = JsonSerializer.Deserialize<BrazilHolidays>(responseBody) };
        }
        catch (HttpRequestException ex)
        {
#if DEBUG
            return new ResponseBase<BrazilHolidays>() { Success = false, Mensage = ex.ToString() };
#else
            return new ResponseBase<BrazilHolidays>() { Success = false, Mensage = "Not valid or inconsistent response." };
#endif
        }
    }

    #endregion Brazil National holidays

    #region FIPE

    /// <summary>
    /// Lists vehicle brands by vehicle type.
    /// </summary>
    /// /// <param name="vehicleType">Vehicle Type Enumerator</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetFIPEBrands(VehicleTypes vehicleType)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Consult the price of the vehicle according to the fipe table.
    /// </summary>
    /// /// <param name="fipeCode">VFIPE Code</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetFIPEPrice(string fipeCode)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Lists existing reference tables.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void GetFIPETable()
    {
        throw new NotImplementedException();
    }

    #endregion FIPE

    #region IBGE

    /// <summary>
    /// Returns the municipalities of the federative unit
    /// </summary>
    /// <param name="uf">Federative unit</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetIBGECodes(string uf)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns information from all states in Brazil
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void GetIBGE()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Search the information of a state from the abbreviation or code
    /// </summary>
    /// <param name="ibgeCode">IBGE Code</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetIBGE(string ibgeCode)
    {
        throw new NotImplementedException();
    }

    #endregion IBGE

    #region ISBN

    /// <summary>
    /// Information about the book from the ISBN
    /// </summary>
    /// <param name="isbn">ISBN Code</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetISBNInfo(string isbn)
    {
        throw new NotImplementedException();
    }

    #endregion ISBN

    #region NCM

    /// <summary>
    /// Returns information from all NCMs
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void GetNCM()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Search for NCMs based on a code or description.
    /// </summary>
    /// <param name="ncmCode">NCM Code</param>
    /// <exception cref="NotImplementedException"></exception>
    public void SearchNCM(string ncmCode)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Search the information of an NCM from a code
    /// </summary>
    /// <param name="ncmCode">NCM Code</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetNCM(string ncmCode)
    {
        throw new NotImplementedException();
    }

    #endregion NCM

    #region REGISTRO BR

    /// <summary>
    /// Evaluates the status of a .br domain, Registro BR
    /// </summary>
    /// <param name="url">Domain URL</param>
    /// <exception cref="NotImplementedException"></exception>
    public void CheckDomainBR(string url)
    {
        throw new NotImplementedException();
    }

    #endregion REGISTRO BR

    #region Brazil Taxes

    /// <summary>
    /// Returns interest rates and some official Brazil indices
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void GetBrTaxes()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Search the information of a tax from its name/acronym
    /// </summary>
    /// <param name="taxName">Tax Name or Acronym</param>
    /// <exception cref="NotImplementedException"></exception>
    public void GetBrTax(string taxName)
    {
        throw new NotImplementedException();
    }

    #endregion Brazil Taxes
}

