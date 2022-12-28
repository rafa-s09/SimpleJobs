using SimpleJobs.Brazil.Documents;

namespace SimpleJobs.Test;

public class BrazilValidationsTests
{
    [Test]
    public void TestCheckForCPF()
    {
        BrazilValidationResult doc = BrazilValidations.CheckForCPF("95327573923");
        BrazilValidationResult docSize = BrazilValidations.CheckForCPF("95327573");
        BrazilValidationResult docInvalid = BrazilValidations.CheckForCPF("95327573921");

        Assert.That(doc == BrazilValidationResult.Success && docSize == BrazilValidationResult.WrongSize && docInvalid == BrazilValidationResult.Failed, Is.True); 
    }

    [Test]
    public void TestCheckForCNPJ()
    {
        BrazilValidationResult doc = BrazilValidations.CheckForCNPJ("88.736.731/0001-40");
        BrazilValidationResult docSize = BrazilValidations.CheckForCNPJ("88.736.731/0001-");
        BrazilValidationResult docInvalid = BrazilValidations.CheckForCNPJ("88.736.731/0001-45");

        Assert.That(doc == BrazilValidationResult.Success && docSize == BrazilValidationResult.WrongSize && docInvalid == BrazilValidationResult.Failed, Is.True);
    }

    [Test]
    public void TestCheckForPIS()
    {
        BrazilValidationResult doc = BrazilValidations.CheckForPIS("120.2451.166-1");
        BrazilValidationResult docSize = BrazilValidations.CheckForPIS("120.2451.166");
        BrazilValidationResult docInvalid = BrazilValidations.CheckForPIS("120.2451.166-7");

        Assert.That(doc == BrazilValidationResult.Success && docSize == BrazilValidationResult.WrongSize && docInvalid == BrazilValidationResult.Failed, Is.True);
    }

}
