using static System.Net.Mime.MediaTypeNames;

namespace SimpleJobs.Security;

/// <summary>
/// Classe para realizar criptografia com AES
/// </summary>
public class EncryptionManager : IDisposable
{
    #region Construtor

    private readonly byte[] hashKey;
    private readonly byte[] iV;

    /// <summary>
    /// Construtor da classe <see cref="EncryptionManager"/>.
    /// </summary>
    /// <param name="password">A senha usada para criar um hash SHA256.</param>
    /// <param name="iv">O vetor de inicialização (IV) opcional usado para criptografia. Se não fornecido, será gerado com um IV fixo.</param>
    public EncryptionManager(string password, string? salt = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));

        try
        {
            // Converte a senha para um hash SHA256
            hashKey = SHA256.HashData(password.StringToByteArray());

            // Valida se o IV é null, usa os ultimos 16 bytes da hash de senha como IV
            salt ??= hashKey[..16].ByteArrayToString();

            // Converte o IV para um hash SHA256
            iV = SHA256.HashData(salt.StringToByteArray())[16..];
        }
        catch
        {
            throw;
        }
    }

    #endregion Construtor

    #region Disposable

    /// <summary>
    /// Destrutor da instância da classe <see cref="EncryptionManager"/>/>.
    /// </summary>
    ~EncryptionManager() => Dispose();

    /// <summary>
    /// Libera os recursos não gerenciados usados pelo <see cref="EncryptionManager"/>/>
    /// e, opcionalmente, libera os recursos gerenciados.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion Disposable

    #region AES

    /// <summary>
    /// Criptografa um array de bytes usando o algoritmo AES.
    /// </summary>
    /// <param name="data">Os dados a serem criptografados.</param>
    /// <returns>Os dados criptografados como um array de bytes.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using, utilizado como separação logica")]
    public byte[] EncryptAES(byte[] data)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = hashKey;
            aesAlg.IV = iV;

            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Descriptografa um array de bytes usando o algoritmo AES.
    /// </summary>
    /// <param name="encryptedData">Os dados criptografados a serem descriptografados.</param>
    /// <returns>Os dados descriptografados como um array de bytes.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using utilizado como separação logica")]
    public byte[] DecryptAES(byte[] encryptedData)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = hashKey;
            aesAlg.IV = iV;

            using (MemoryStream msDecrypt = new(encryptedData))
            {
                using (CryptoStream csDecrypt = new(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (MemoryStream ms = new())
                    {
                        csDecrypt.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Criptografa uma string usando o algoritmo AES.
    /// </summary>
    /// <param name="text">A string a ser criptografada.</param>
    /// <returns>A string criptografada.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using utilizado como separação logica")]
    public string EncryptAESText(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = hashKey;
            aesAlg.IV = iV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new(csEncrypt))
                        swEncrypt.Write(text);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    /// <summary>
    /// Descriptografa uma string usando o algoritmo AES.
    /// </summary>
    /// <param name="encryptedText">A string criptografada a ser descriptografada.</param>
    /// <returns>A string descriptografada.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using utilizado como separação logica")]
    public string DecryptAESText(string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText))
            return string.Empty;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = hashKey;
            aesAlg.IV = iV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msDecrypt = new(Convert.FromBase64String(encryptedText)))
            {
                using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new(csDecrypt))
                        return srDecrypt.ReadToEnd();
                }
            }
        }
    }

    #endregion AES
}