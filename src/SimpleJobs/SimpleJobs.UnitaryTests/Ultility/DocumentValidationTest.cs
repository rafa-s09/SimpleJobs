namespace SimpleJobs.UnitaryTests.Ultility;

public class DocumentValidationTest : TestBase
{
    #region Validadores

    [Theory]
    [InlineData("56391306796", DocumentValidationResponse.Valid)]
    [InlineData("563.913.067-96", DocumentValidationResponse.Valid)]
    [InlineData("563913067", DocumentValidationResponse.WrongSize)]
    [InlineData("563.913.0676", DocumentValidationResponse.WrongSize)]
    [InlineData("", DocumentValidationResponse.Invalid)]
    [InlineData("56391306795", DocumentValidationResponse.Invalid)]
    [InlineData("563.913.067-95", DocumentValidationResponse.Invalid)]
    public void CPFIsValid_CheckDocumenteValidationOk(string cpf, DocumentValidationResponse expectedResult)
    {
        DocumentValidationResponse result = DocumentValidation.CPFIsValid(cpf);
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("48391445000176", DocumentValidationResponse.Valid)]
    [InlineData("48.391.445/0001-76", DocumentValidationResponse.Valid)]
    [InlineData("483914450001", DocumentValidationResponse.WrongSize)]
    [InlineData("48.391.445/001-76", DocumentValidationResponse.WrongSize)]
    [InlineData("", DocumentValidationResponse.Invalid)]
    [InlineData("48391445000173", DocumentValidationResponse.Invalid)]
    [InlineData("48.391.445/0001-73", DocumentValidationResponse.Invalid)]
    public void CNPJIsValid_CheckDocumentValidationOk(string cnpj, DocumentValidationResponse expectedResult)
    {
        DocumentValidationResponse result = DocumentValidation.CNPJIsValid(cnpj);
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("12025477440", DocumentValidationResponse.Valid)]
    [InlineData("120.2547.744-0", DocumentValidationResponse.Valid)]
    [InlineData("1205477440", DocumentValidationResponse.WrongSize)]
    [InlineData("120.547.744-0", DocumentValidationResponse.WrongSize)]
    [InlineData("", DocumentValidationResponse.Invalid)]
    [InlineData("12025477446", DocumentValidationResponse.Invalid)]
    [InlineData("120.2547.744-6", DocumentValidationResponse.Invalid)]
    public void PISsValid_CheckDocumentValidationOk(string pis, DocumentValidationResponse expectedResult)
    {
        DocumentValidationResponse result = DocumentValidation.PISsValid(pis);
        result.Should().Be(expectedResult);
    }

    #endregion Validadores
}
