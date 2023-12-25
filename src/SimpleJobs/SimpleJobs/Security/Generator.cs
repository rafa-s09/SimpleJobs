namespace SimpleJobs.Security;

/// <summary>
/// Gera textos e GUID aleatório
/// </summary>
public static class Generator
{
    /// <summary>
    /// Gera um texto aleatório com base em um valor numérico e opção de incluir caracteres especiais.
    /// </summary>    
    /// <param name="length">O comprimento do texto gerado.</param>
    /// <param name="includeSpecialCharacters">Indica se caracteres especiais devem ser incluídos. . O padrão é false</param>
    /// <returns>O texto aleatório gerado.</returns>
    public static string RandomText(int length, bool includeSpecialCharacters = false)
    {
        const string caracteresNormais = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const string caracteresEspeciais = "!@#$%^&*";
        string caracteres = includeSpecialCharacters ? (caracteresNormais + caracteresEspeciais) : caracteresNormais;

        StringBuilder sb = new();
        Random rnd = new();

        for (int i = 0; i < length; i++)
        {
            int indice = rnd.Next(caracteres.Length);
            sb.Append(caracteres[indice]);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Gera um identificador único (UID) composto por quatro partes de texto aleatório separadas por hífens. <br/>
    /// Exemplo de UID: 8XA-PWLN4-6Y8WB-CEFN
    /// </summary>
    /// <returns>Um UID gerado aleatoriamente.</returns>
    public static string Uid()
    {
        StringBuilder sb = new();
        sb.Append(RandomText(3));
        sb.Append('-');
        sb.Append(RandomText(5));
        sb.Append('-');
        sb.Append(RandomText(5));
        sb.Append('-');
        sb.Append(RandomText(4));

        return sb.ToString().ToUpper();
    }

    /// <summary>
    /// Gera um GUID (identificador global único) padrão.
    /// </summary>
    /// <param name="includeSpecialCharacters">Indica se caracteres especiais devem ser incluídos no GUID. O padrão é true.</param>
    /// <returns>Um GUID gerado.</returns>
    public static string DefaultGuid(bool includeSpecialCharacters = true)
    {
        Guid guid = Guid.NewGuid();
        return includeSpecialCharacters ? guid.ToString() : guid.ToString().Replace("-", "");
    }

}
