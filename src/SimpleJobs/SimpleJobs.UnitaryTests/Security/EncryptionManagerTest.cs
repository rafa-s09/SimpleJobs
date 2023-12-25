namespace SimpleJobs.UnitaryTests.Security;

public class EncryptionManagerTests : IDisposable
{
    private readonly EncryptionManager encryptionManager;

    public EncryptionManagerTests()
    {
        // Configuração comum para os testes, se necessário
        encryptionManager = new EncryptionManager("Lorem&Ipsum");
    }

    // Limpeza de recursos, se necessário
    public void Dispose()
    {
        encryptionManager.Dispose();
        GC.SuppressFinalize(this);  
    }

    [Fact]
    public void EncryptAES_DecryptAES_Success()
    {
        string originalData = "Hello, World!";
        byte[] originalBytes = Encoding.UTF8.GetBytes(originalData);

        byte[] encryptedBytes = encryptionManager.EncryptAES(originalBytes);
        byte[] decryptedBytes = encryptionManager.DecryptAES(encryptedBytes);

        string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
        Assert.Equal(originalData, decryptedData);
    }

    [Fact]
    public void EncryptAESText_DecryptAESText_Success()
    {
        string originalData = "Hello, World!";

        string encryptedText = encryptionManager.EncryptAESText(originalData);
        string decryptedText = encryptionManager.DecryptAESText(encryptedText);

        Assert.Equal(originalData, decryptedText);
    }
}
