namespace SimpleJobs.Security;

/// <summary>
/// Classe para realizar criptografia com os algoritmo RSA atraves de um certificado X509.
/// </summary>
public class SignedEncryptionManager : IDisposable
{
    #region Construtor

    private readonly RSA publicKey;

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    public SignedEncryptionManager(X509Certificate2 certificate, bool checkCertificateValidity = false)
    {
        // Verifica se o certificate é valido
        ArgumentNullException.ThrowIfNull(certificate);

        if (checkCertificateValidity)
        {
            // Valida a data do certificado
            DateTime currentDateUtc = DateTime.UtcNow;
            if (!(currentDateUtc >= certificate.NotBefore && currentDateUtc <= certificate.NotAfter))
                throw new InvalidOperationException("O certificado não é mais válido.");
        }

        if (certificate.GetRSAPrivateKey() == null)
            throw new InvalidOperationException("O certificado não possui uma chave privada RSA.");

#pragma warning disable CS8601 // Possível atribuição de referência nula.
        publicKey = certificate.GetRSAPrivateKey();
#pragma warning restore CS8601 // Possível atribuição de referência nula.
    }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

    #endregion Construtor

    #region Disposable

    /// <summary>
    /// Destrutor da instância da classe <see cref="SignedEncryptionManager"/>/>.
    /// </summary>
    ~SignedEncryptionManager() => Dispose();

    /// <summary>
    /// Libera os recursos não gerenciados usados pelo <see cref="SignedEncryptionManager"/>/>
    /// e, opcionalmente, libera os recursos gerenciados.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Libera os recursos não gerenciados usados pelo <see cref="SignedEncryptionManager"/>/>
    /// e, opcionalmente, libera os recursos gerenciados.
    /// </summary>
    /// <param name="disposing">True para liberar os recursos gerenciados e não gerenciados; false para liberar apenas os recursos não gerenciados.</param>
    protected virtual void Dispose(bool disposing = false)
    {
        // TODO: Liberar quaisquer recursos não gerenciados aqui.
        if (disposing)
        {
            // TODO: Liberar quaisquer recursos gerenciados aqui.
        }
    }

    #endregion Disposable

    #region RSA

    /// <summary>
    /// Criptografa dados usando a chave pública RSA de um certificado X.509.
    /// </summary>
    /// <param name="data">Os dados a serem criptografados.</param>
    /// <returns>Os dados criptografados.</returns>
    public byte[] Encrypt(byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);

        using RSA rsa = publicKey;
        return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
    }

    /// <summary>
    /// Descriptografa dados usando a chave privada RSA de um certificado X.509.
    /// </summary>
    /// <param name="data">Os dados a serem descriptografados.</param>
    /// <returns>Os dados descriptografados.</returns>
    public byte[] Decrypt(byte[] encryptedData)
    {
        ArgumentNullException.ThrowIfNull(encryptedData);

        using RSA rsa = publicKey;
        return rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
    }

    /// <summary>
    /// Criptografa uma string usando a chave pública RSA de um certificado X.509.
    /// </summary>
    /// <param name="data">A string a ser criptografada.</param>
    /// <returns>A string criptografada.</returns>
    public string EncryptText(string data)
    {
        // Verifica se a string de entrada é nula
        ArgumentNullException.ThrowIfNull(data);

        // Converte a string para um array de bytes, criptografa e converte de volta para string
        return Encrypt(data.StringToByteArray()).ByteArrayToString();
    }

    /// <summary>
    /// Descriptografa uma string usando a chave pública RSA de um certificado X.509.
    /// </summary>
    /// <param name="encryptedData">A string criptografada a ser descriptografada.</param>
    /// <returns>A string descriptografada.</returns>
    public string DecryptText(string encryptedData)
    {
        // Verifica se a string de entrada é nula
        ArgumentNullException.ThrowIfNull(encryptedData);

        // Converte a string criptografada para um array de bytes, descriptografa e converte de volta para string
        return Decrypt(encryptedData.StringToByteArray()).ByteArrayToString();
    }

    #endregion RSA
}
