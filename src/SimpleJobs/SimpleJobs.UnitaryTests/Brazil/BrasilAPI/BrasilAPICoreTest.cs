namespace SimpleJobs.UnitaryTests.Brazil.BrasilAPI;

public class BrasilAPICoreTest : BaseTest
{
    [Theory]
    [InlineData("1", true)]
    [InlineData("x", false)]
    [InlineData("", false)]
    public async void GetBank_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilAPICore.GetBank(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(Bank));
        else
            result.Content.Should().Be(null);
    }

    [Fact]
    public async void GetBanks_SimpleTest()
    {
        var result = await BrasilAPICore.GetBanks();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<Bank>));
    }

    [Theory]
    [InlineData("89010025", true)]
    [InlineData("89010-025", true)]
    [InlineData("890100", false)]
    [InlineData("", false)]
    public async void GetCEP_SimpleTest(string cep, bool expectedResult)
    {
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
        var result = await BrasilAPICore.GetCep(cep);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto

        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(Cep));
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
        var result = await BrasilAPICore.GetCepV2(cep);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(Cep));
        else
            result.Content.Should().Be(null);
    }

    [Theory]
    [InlineData("47960950108836", true)]
    [InlineData("47.960.950/1088-36", true)]
    [InlineData("483914450001", false)]
    [InlineData("48.391.445/001-76", false)]
    [InlineData("", false)]
    [InlineData("48391445000173", false)]
    [InlineData("48.391.445/0001-73", false)]
    public async void GetCNPJInfo_SimpleTest(string cnpj, bool expectedResult)
    {
        var result = await BrasilAPICore.GetCNPJInfo(cnpj);
        result.Success.Should().Be(expectedResult);

        if (!expectedResult)
        {
            result.Content.Should().Be(null);
            if(string.IsNullOrEmpty(cnpj.Replace(" ", "")))
                result.Mensage.Should().Be("The entered value cannot be null or empty.");
            else
                result.Mensage.Should().Be("Wrong CNPJ Number");
        }
        else
        {
            result.Content.Should().BeOfType(typeof(Cnpj));
            result.Mensage.Should().Be("Ok");
        }
    }


    [Theory]
    [InlineData(11, true)]
    [InlineData(0, false)]
    public async void GetDDD_SimpleTest(int ddd, bool expectedResult)
    {
        var result = await BrasilAPICore.GetDDD(ddd);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(DDD));
        else
            result.Content.Should().Be(null);
    }

    [Theory]
    [InlineData(2023, true)]
    [InlineData(0, false)]
    public async void GetHolidays_SimpleTest(int year, bool expectedResult)
    {
        var result = await BrasilAPICore.GetHolidays(year);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<BrazilHolidays>));        
        else
            result.Content.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData(VehicleTypes.Truck, true)]
    [InlineData(VehicleTypes.Bike, true)]
    [InlineData(VehicleTypes.Car, true)]
    public async void GetFIPEBrands_SimpleTest(VehicleTypes vehicleType, bool expectedResult)
    {
        var result = await BrasilAPICore.GetFIPEBrands(vehicleType);
        result.Success.Should().Be(expectedResult);
        result.Content.Should().BeOfType(typeof(List<FipeBrands>));
    }

    [Theory]
    [InlineData("003283-2", true)]
    [InlineData("207", false)]
    [InlineData("", false)]
    public async void GetFIPEPrice_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilAPICore.GetFIPEPrice(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<FipeVehicle>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    [Fact]
    public async void GetFIPETable_SimpleTest()
    {
        var result = await BrasilAPICore.GetFIPETable();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<FipeTables>));
    }

    [Fact]
    public async void GetIBGE_SimpleTest()
    {
        var result = await BrasilAPICore.GetIBGE();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<Ibge>));
    }

    [Theory]
    [InlineData("33", true)]
    [InlineData("404", false)]
    [InlineData("", false)]
    public async void GetIBGE_ByCode_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilAPICore.GetIBGE(code);
        result.Success.Should().Be(expectedResult);
        if(expectedResult)
            result.Content.Should().BeOfType(typeof(Ibge));
        else
            result.Content.Should().Be(null);
    }

    [Theory]
    [InlineData("SP", true)]
    [InlineData("sp", true)]
    [InlineData("xX", false)]
    [InlineData("X", false)]
    [InlineData("", false)]
    public async void GetIBGECodes_SimpleTest(string uf, bool expectedResult)
    {
        var result = await BrasilAPICore.GetIBGECodes(uf);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<IbgeCities>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData("9788545702870", true)]
    [InlineData("404", false)]
    [InlineData("", false)]
    public async void GetISBNInfo_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilAPICore.GetISBNInfo(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(Isbn));
        else
            result.Content.Should().Be(null);
    }

    [Theory]
    [InlineData("17011400", true)]
    [InlineData("170", true)]
    [InlineData("", false)]
    public async void SearchNCM_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilAPICore.SearchNCM(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(List<Ncm>));
        else
            result.Content.Should().BeNullOrEmpty();
    }

    [Fact]
    public async void GetNCM_SimpleTest()
    {
        var result = await BrasilAPICore.GetNCM();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<Ncm>));
    }

    [Theory]
    [InlineData("17011400", true)]
    [InlineData("404", false)]
    [InlineData("", false)]
    public async void GetNCM_ByCode_SimpleTest(string code, bool expectedResult)
    {
        var result = await BrasilAPICore.GetNCM(code);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(Ncm));
        else
            result.Content.Should().Be(null);
    }

    //NOTE: Can't find a way to return 400 error code
    [Theory]
    [InlineData("www.google.com.br", true)]
    [InlineData("", false)] 
    public async void CheckDomainBR_SimpleTest(string url, bool expectedResult)
    {
        var result = await BrasilAPICore.CheckDomainBR(url);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(DomainBr));
        else
            result.Content.Should().Be(null);
    }

    [Fact]
    public async void GetBrTaxes_SimpleTest()
    {
        var result = await BrasilAPICore.GetBrTaxes();
        result.Success.Should().BeTrue();
        result.Content.Should().BeOfType(typeof(List<BrazilTaxes>));
    }

    [Theory]
    [InlineData("cdi", true)]
    [InlineData("bbb", false)]
    [InlineData("", false)]
    public async void GetBrTax_SimpleTest(string taxName, bool expectedResult)
    {
        var result = await BrasilAPICore.GetBrTax(taxName);
        result.Success.Should().Be(expectedResult);
        if (expectedResult)
            result.Content.Should().BeOfType(typeof(BrazilTaxes));
        else
            result.Content.Should().Be(null);
    }

}


