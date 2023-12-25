namespace SimpleJobs.UnitaryTests.BrasilAPI;

public class BrasilApiRequestTest : TestBase
{
    #region Bank
    
    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public async void GetBank_SimpleTest(int code, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetBank(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(BankResponse));
        else
            result.Content.Should().Be(null);
    }

    [Fact]
    public async void GetBanks_SimpleTest()
    {
        var result = await BrasilApiRequest.GetBanks();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<BankResponse>));
    }

    #endregion Bank

    #region CEP
    
    [Theory]
    [InlineData("89010025", true)]
    [InlineData("89010-025", true)]
    [InlineData("890100", false)]
    [InlineData("", false)]
    public async void GetCEP_SimpleTest(string cep, bool expectedResult)
    {
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
        var result = await BrasilApiRequest.GetCep(cep);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto

        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(CepResponse));
        else
            result.Content.Should().Be(null);
    }

    [Theory]
    [InlineData("89010025", true)]
    [InlineData("89010-025", true)]
    [InlineData("890100", false)]
    [InlineData("", false)]
    public async void GetCEPV2_SimpleTest(string cep, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetCepV2(cep);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(CepResponse));
        else
            result.Content.Should().Be(null);
    }

    #endregion CEP

    #region CNPJ

    [Theory]
    [InlineData("47960950108836", true)]
    [InlineData("47.960.950/1088-36", true)]
    [InlineData("483914450001", false)]
    [InlineData("48.391.445/001-76", false)]
    [InlineData("", false)]
    [InlineData("48391445000173", false)]
    [InlineData("48.391.445/0001-73", false)]
    public async void GetCNPJ_SimpleTest(string cnpj, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetCNPJ(cnpj);
        result.Success.Should().Be(expectedResult);

        if (!expectedResult)
        {
            result.Content.Should().Be(null);
            if (string.IsNullOrEmpty(cnpj.Replace(" ", "")))
                result.Message.Should().Be("O valor inserido não pode ser nulo ou vazio.");
            else
                result.Message.Should().Be("O número fornecido do CNPJ é inválido.");
        }
        else
        {
            result.Content.Should().BeOfType(typeof(CnpjResponse));
            result.Success.Should().BeTrue();
        }
    }

    #endregion CNPJ

    #region Corretoras

    [Fact]
    public async void GetCorretoras_SimpleTest()
    {
        var result = await BrasilApiRequest.GetCorretoras();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<CorretoraResponse>));
    }

    [Theory]
    [InlineData("02332886000104", true)]
    [InlineData("02.332.886/0001-04", true)]
    [InlineData("023328860001", false)]
    [InlineData("02.332.886/0001-44", false)]
    [InlineData("", false)]
    [InlineData("02332886000144", false)]
    public async void GetCorretora_SimpleTest(string cnpj, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetCorretora(cnpj);
        result.Success.Should().Be(expectedResult);

        if (!expectedResult)
        {
            result.Content.Should().Be(null);
            if (string.IsNullOrEmpty(cnpj.Replace(" ", "")))
                result.Message.Should().Be("O valor inserido não pode ser nulo ou vazio.");
            else
                result.Message.Should().Be("O número fornecido do CNPJ é inválido.");
        }
        else
        {
            result.Content.Should().BeOfType(typeof(CorretoraResponse));
            result.Success.Should().BeTrue();
        }
    }

    #endregion Corretoras

    #region CPTEC

    [Fact]
    public async void GetCPTECCities_SimpleTest()
    {
        var result = await BrasilApiRequest.GetCPTECCities();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<CPTECCityResponse>));
    }

    [Theory]
    [InlineData("São Benedito", true)]
    [InlineData("Benedito", true)]
    [InlineData("", false)]
    public async void GetCPTECCities_ByName_SimpleTest(string city, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetCPTECCities(city);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<CPTECCityResponse>));
        else
            result.Content.Should().BeNull();
    }

    [Fact]
    public async void GetCPTECCapital_SimpleTest()
    {
        var result = await BrasilApiRequest.GetCPTECCapital();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<CPTECAirportResponse>));
    }

    [Theory]
    [InlineData("SBGR", true)]
    [InlineData("0000", false)]
    [InlineData("", false)]
    public async void GetCPTECAirport_SimpleTest(string icao, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetCPTECAirport(icao);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(CPTECAirportResponse));
        else
            result.Content.Should().BeNull();
    }

    [Theory]
    [InlineData("244", 0, true)]
    [InlineData("000", 0, false)]
    [InlineData("244", 3, true)]
    [InlineData("000", 3, false)]
    [InlineData("244", 9, true)]
    [InlineData("000", 9, false)]
    public async void GetCPTECCityWeather_SimpleTest(string cityCode, int days, bool expectedResult)
    {
        BrasilApiResponse<CPTECForecastResponse>? result;
        if(days > 0)
            result = await BrasilApiRequest.GetCPTECCityWeather(cityCode, days);
        else
            result = await BrasilApiRequest.GetCPTECCityWeather(cityCode);

        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(CPTECForecastResponse));
        else
            result.Content.Should().BeNull();
    }

    [Theory]
    [InlineData("244", 0, true)]
    [InlineData("000", 0, false)]
    [InlineData("244", 3, true)]
    [InlineData("000", 3, false)]
    [InlineData("244", 9, true)]
    [InlineData("000", 9, false)]
    public async void GetCPTECOceanWeather_SimpleTest(string cityCode, int days, bool expectedResult)
    {
        BrasilApiResponse<CPTECOceanResponse>? result;
        if (days > 0)
            result = await BrasilApiRequest.GetCPTECOceanWeather(cityCode, days);
        else
            result = await BrasilApiRequest.GetCPTECOceanWeather(cityCode);

        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(CPTECOceanResponse));
        else
            result.Content.Should().BeNull();
    }

    #endregion CPTEC

    #region DDD

    [Theory]
    [InlineData(11, true)]
    [InlineData(0, false)]
    public async void GetDDD_SimpleTest(int ddd, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetDDD(ddd);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(DDDResponse));
        else
            result.Content.Should().Be(null);
    }

    #endregion DDD

    #region Feriados Nacionais

    [Theory]
    [InlineData(2023, true)]
    [InlineData(0, false)]
    public async void GetFeriadosNacionais_SimpleTest(int year, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetFeriadosNacionais(year);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<FeriadosNacionaisReponse>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    #endregion Feriados Nacionais

    #region FIPE

    [Theory]
    [InlineData(VehicleTypes.Truck, true)]
    [InlineData(VehicleTypes.Bike, true)]
    [InlineData(VehicleTypes.Car, true)]
    public async void GetFIPEBrands_SimpleTest(VehicleTypes vehicleType, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetFIPETipoVeiculo(vehicleType);
        result.Success.Should().Be(expectedResult);
        result.Content.Should().BeOfType(typeof(List<FIPEVehicleTypeResponse>));
    }

    [Theory]
    [InlineData("003283-2", true)]
    [InlineData("207", false)]
    [InlineData("", false)]
    public async void GetFIPEPrice_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetFIPE(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<FIPEVehicleResponse>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    [Fact]
    public async void GetFIPETable_SimpleTest()
    {
        var result = await BrasilApiRequest.GetFIPETables();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<FIPETableResponse>));
    }

    #endregion FIPE

    #region IBGE

    [Fact]
    public async void GetIBGE_SimpleTest()
    {
        var result = await BrasilApiRequest.GetIBGE();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<IBGEResponse>));
    }

    [Theory]
    [InlineData("33", true)]
    [InlineData("404", false)]
    [InlineData("", false)]
    public async void GetIBGE_ByCode_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetIBGE(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(IBGEResponse));
        else
            result.Content.Should().Be(null);
    }

    [Theory]
    [InlineData("SP", true)]
    [InlineData("sp", true)]
    [InlineData("xX", false)]
    [InlineData("X", false)]
    [InlineData("", false)]
    public async void GetIBGERegions_SimpleTest(string uf, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetIBGERegions(uf);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<IBGERegionsResponse>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    #endregion IBGE

    #region ISBN

    [Theory]
    [InlineData("9788545702870", true)]
    [InlineData("404", false)]
    [InlineData("", false)]
    public async void GetISBN_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetISBN(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(ISBNResponse));
        else
            result.Content.Should().Be(null);
    }

    #endregion ISBN

    #region NCM

    [Theory]
    [InlineData("17011400", true)]
    [InlineData("170", true)]
    [InlineData("", false)]
    public async void SearchNCM_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilApiRequest.SearchNCM(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<NCMResponse>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    [Fact]
    public async void GetNCM_SimpleTest()
    {
        var result = await BrasilApiRequest.GetNCM();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<NCMResponse>));
    }

    [Theory]
    [InlineData("17011400", true)]
    [InlineData("404", false)]
    [InlineData("", false)]
    public async void GetNCM_ByCode_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetNCM(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(NCMResponse));
        else
            result.Content.Should().Be(null);
    }

    #endregion NCM

    #region PIX

    [Fact]
    public async void GetPIX_SimpleTest()
    {
        var result = await BrasilApiRequest.GetPIX();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<PIXResponse>));
    }

    #endregion PIX

    #region REGISTRO BR
    
    [Theory]
    [InlineData("www.google.com.br", true)]
    [InlineData("", false)]
    public async void CheckRegistroBr_SimpleTest(string url, bool expectedResult)
    {
        var result = await BrasilApiRequest.CheckRegistroBr(url);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(RegistroBrResponse));
        else
            result.Content.Should().Be(null);
    }

    #endregion REGISTRO BR

    #region TAXAS

    [Fact]
    public async void GetTaxes_SimpleTest()
    {
        var result = await BrasilApiRequest.GetTaxes();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<TaxasResponse>));
    }

    [Theory]
    [InlineData("cdi", true)]
    [InlineData("bbb", false)]
    [InlineData("", false)]
    public async void GetTaxes_ByName_SimpleTest(string taxName, bool expectedResult)
    {
        var result = await BrasilApiRequest.GetTaxes(taxName);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(TaxasResponse));
        else
            result.Content.Should().Be(null);
    }

    #endregion TAXAS
}
