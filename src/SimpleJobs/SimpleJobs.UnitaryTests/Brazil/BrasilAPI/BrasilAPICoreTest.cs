using SimpleJobs.Brazil.BrasilAPI;
using Xunit;

namespace SimpleJobs.UnitaryTests.Brazil.BrasilAPI;

public class BrasilAPICoreTest : BaseTest
{
    [Theory]
    [InlineData("1", true)]
    [InlineData("x", false)]
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
    [InlineData(null, false)]
    public async void GetCNPJInfo_SimpleTest(string cnpj, bool expectedResult)
    {
        var result = await BrasilAPICore.GetCNPJInfo(cnpj);
        result.Success.Should().Be(expectedResult);

        if (!expectedResult)
        {
            result.Content.Should().Be(null);
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


}
