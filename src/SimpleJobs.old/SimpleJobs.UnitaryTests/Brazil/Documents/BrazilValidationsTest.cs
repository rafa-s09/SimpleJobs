namespace SimpleJobs.UnitaryTests.Brazil.Documents;

public class BrazilValidationsTest : BaseTest
{

    [Theory]
    [InlineData("56391306796", BrazilValidationResult.Success)]
    [InlineData("563.913.067-96", BrazilValidationResult.Success)]
    [InlineData("563913067", BrazilValidationResult.WrongSize)]
    [InlineData("563.913.0676", BrazilValidationResult.WrongSize)]
    [InlineData("", BrazilValidationResult.Failed)]
    [InlineData("56391306795", BrazilValidationResult.Failed)]
    [InlineData("563.913.067-95", BrazilValidationResult.Failed)]
    public void CheckForCPF_CheckDocumenteValidationOk(string cpf, BrazilValidationResult expectedResult)
    {
        BrazilValidationResult result = BrazilValidations.CheckForCPF(cpf);
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("48391445000176", BrazilValidationResult.Success)]
    [InlineData("48.391.445/0001-76", BrazilValidationResult.Success)]
    [InlineData("483914450001", BrazilValidationResult.WrongSize)]
    [InlineData("48.391.445/001-76", BrazilValidationResult.WrongSize)]
    [InlineData("", BrazilValidationResult.Failed)]
    [InlineData("48391445000173", BrazilValidationResult.Failed)]
    [InlineData("48.391.445/0001-73", BrazilValidationResult.Failed)]
    public void CheckForCNPJ_CheckDocumentValidationOk(string cnpj, BrazilValidationResult expectedResult)
    {
        BrazilValidationResult result = BrazilValidations.CheckForCNPJ(cnpj);
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("12025477440", BrazilValidationResult.Success)]
    [InlineData("120.2547.744-0", BrazilValidationResult.Success)]
    [InlineData("1205477440", BrazilValidationResult.WrongSize)]
    [InlineData("120.547.744-0", BrazilValidationResult.WrongSize)]
    [InlineData("", BrazilValidationResult.Failed)]
    [InlineData("12025477446", BrazilValidationResult.Failed)]
    [InlineData("120.2547.744-6", BrazilValidationResult.Failed)]
    public void CheckForPIS_CheckDocumentValidationOk(string pis, BrazilValidationResult expectedResult)
    {
        BrazilValidationResult result = BrazilValidations.CheckForPIS(pis);
        result.Should().Be(expectedResult);
    }
}
