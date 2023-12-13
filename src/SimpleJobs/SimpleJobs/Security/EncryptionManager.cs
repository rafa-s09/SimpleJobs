namespace SimpleJobs.Security;

/// <summary>
/// Classe para realizar criptografia com os algoritmos AES ou TripleDES (3DES).
/// </summary>
public class EncryptionManager : IDisposable
{
    #region Construtor

    private readonly string passwordHash;
    private readonly string IV;

    /// <summary>
    /// Construtor da classe <see cref="EncryptionManager"/>.
    /// </summary>
    /// <param name="password">A senha usada para criar um hash MD5.</param>
    /// <param name="iv">O vetor de inicialização (IV) opcional usado para criptografia. Se não fornecido, será gerado com um IV fixo.</param>
    public EncryptionManager(string password, string? iv = null)
    {
        // Converte a senha para um hash MD5
        byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(password));
        passwordHash = hashBytes.ByteArrayToString().Replace("-", "")[..16];

        // Valida se o IV é null, usa os ultimos 16 bytes da hash de senha como IV
        iv ??= passwordHash.Substring(16, 16);

        // Converte o IV para um hash MD5
        byte[] hashIVBytes = MD5.HashData(Encoding.UTF8.GetBytes(iv));
        IV = hashIVBytes.ByteArrayToString().Replace("-", "").Substring(16, 16);
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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Libera os recursos não gerenciados usados pelo <see cref="EncryptionManager"/>/>
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

    #region AES

    /// <summary>
    /// Criptografa um array de bytes usando o algoritmo AES.
    /// </summary>
    /// <param name="data">Os dados a serem criptografados.</param>
    /// <returns>Os dados criptografados como um array de bytes.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using utilizado como separação logica")]
    public byte[] EncryptAES(byte[] data)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(passwordHash);
            aesAlg.IV = Encoding.UTF8.GetBytes(IV);

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
            aesAlg.Key = Encoding.UTF8.GetBytes(passwordHash);
            aesAlg.IV = Encoding.UTF8.GetBytes(IV);

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
    /// <param name="data">A string a ser criptografada.</param>
    /// <returns>A string criptografada.</returns>
    public string EncryptAESText(string data)
    {
        // Verifica se a string de entrada é nula
        ArgumentNullException.ThrowIfNull(data);

        // Converte a string para um array de bytes, criptografa e converte de volta para string
        return EncryptAES(data.StringToByteArray()).ByteArrayToString();
    }

    /// <summary>
    /// Descriptografa uma string usando o algoritmo AES.
    /// </summary>
    /// <param name="encryptedData">A string criptografada a ser descriptografada.</param>
    /// <returns>A string descriptografada.</returns>
    public string DecryptAESText(string encryptedData)
    {
        // Verifica se a string de entrada é nula
        ArgumentNullException.ThrowIfNull(encryptedData);

        // Converte a string criptografada para um array de bytes, descriptografa e converte de volta para string
        return DecryptAES(encryptedData.StringToByteArray()).ByteArrayToString();
    }

    #endregion AES

    #region TripleDES (3DES)

    /// <summary>
    /// Criptografa um array de bytes usando o algoritmo TripleDES (3DES).
    /// </summary>
    /// <param name="data">Os dados a serem criptografados.</param>
    /// <returns>Os dados criptografados como um array de bytes.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using utilizado como separação logica")]
    public byte[] EncryptTripleDES(byte[] data)
    {
        using (TripleDES tripleAld = TripleDES.Create())
        {
            tripleAld.Key = Encoding.UTF8.GetBytes(passwordHash);
            tripleAld.IV = Encoding.UTF8.GetBytes(IV);

            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, tripleAld.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Descriptografa um array de bytes usando o algoritmo TripleDES (3DES).
    /// </summary>
    /// <param name="encryptedData">Os dados criptografados a serem descriptografados.</param>
    /// <returns>Os dados descriptografados como um array de bytes.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Usar a instrução 'using' simples", Justification = "Blocos de using utilizado como separação logica")]
    public byte[] DecryptTripleDES(byte[] encryptedData)
    {
        using (TripleDES tripleAld = TripleDES.Create())
        {
            tripleAld.Key = Encoding.UTF8.GetBytes(passwordHash);
            tripleAld.IV = Encoding.UTF8.GetBytes(IV);

            using (MemoryStream msDecrypt = new(encryptedData))
            {
                using (CryptoStream csDecrypt = new(msDecrypt, tripleAld.CreateDecryptor(), CryptoStreamMode.Read))
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
    /// Criptografa uma string usando o algoritmo TripleDES (3DES).
    /// </summary>
    /// <param name="data">A string a ser criptografada.</param>
    /// <returns>A string criptografada.</returns>
    public string EncryptTripleDESText(string data)
    {
        // Verifica se a string de entrada é nula
        ArgumentNullException.ThrowIfNull(data);

        // Converte a string para um array de bytes, criptografa e converte de volta para string
        return EncryptTripleDES(data.StringToByteArray()).ByteArrayToString();
    }

    /// <summary>
    /// Descriptografa uma string usando o algoritmo TripleDES (3DES).
    /// </summary>
    /// <param name="encryptedData">A string criptografada a ser descriptografada.</param>
    /// <returns>A string descriptografada.</returns>
    public string DecryptTripleDESText(string encryptedData)
    {
        // Verifica se a string de entrada é nula
        ArgumentNullException.ThrowIfNull(encryptedData);

        // Converte a string criptografada para um array de bytes, descriptografa e converte de volta para string
        return DecryptTripleDES(encryptedData.StringToByteArray()).ByteArrayToString();
    }

    #endregion TripleDES (3DES)

}