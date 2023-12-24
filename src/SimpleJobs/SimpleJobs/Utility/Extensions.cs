namespace SimpleJobs.Utility;

/// <summary>
/// Contem varias extenções uteis
/// </summary>
public static partial class Extensions
{
    #region Text

    /// <summary>
    /// Obtém a substring da string fornecida até o primeiro caractere especificado (exclusivo).
    /// </summary>
    /// <param name="text">A string de origem.</param>
    /// <param name="stopAt">O caractere que determina o limite para a substring.</param>
    /// <returns>A substring da string original até o primeiro caractere especificado (exclusivo), ou uma string vazia se a string original estiver vazia ou o caractere não for encontrado.</returns>
    public static string GetUntilOrEmpty(this string text, char stopAt)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);
        return charLocation > 0 ? text[..charLocation] : string.Empty;
    }

    /// <summary>
    /// Obtém a substring da string fornecida até o primeiro caractere especificado (inclusivo).
    /// </summary>
    /// <param name="text">A string de origem.</param>
    /// <param name="stopAt">O caractere que determina o limite para a substring.</param>
    /// <returns>A substring da string original até o primeiro caractere especificado (inclusivo), ou a string original se estiver vazia ou o caractere não for encontrado.</returns>
    public static string GetUntil(this string text, char stopAt)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);
        return charLocation > 0 ? text[..charLocation] : text;

    }

    /// <summary>
    /// Obtém a substring da string fornecida após o primeiro caractere especificado (exclusivo).
    /// </summary>
    /// <param name="text">A string de origem.</param>
    /// <param name="startAt">O caractere que determina o início da substring.</param>
    /// <returns>A substring da string original após o primeiro caractere especificado (exclusivo), ou uma string vazia se a string original estiver vazia ou o caractere não for encontrado.</returns>
    public static string GetAfterOrEmpty(this string text, char startAt)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        int charLocation = text.IndexOf(startAt, StringComparison.Ordinal);
        return charLocation > 0 ? text[(charLocation + 1)..] : string.Empty;
    }

    /// <summary>
    /// Obtém a substring da string fornecida após o primeiro caractere especificado (inclusivo).
    /// </summary>
    /// <param name="text">A string de origem.</param>
    /// <param name="startAt">O caractere que determina o início da substring.</param>
    /// <returns>A substring da string original após o primeiro caractere especificado (inclusivo), ou a string original se estiver vazia ou o caractere não for encontrado.</returns>
    public static string GetAfter(this string text, char startAt)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        int charLocation = text.IndexOf(startAt, StringComparison.Ordinal);
        return charLocation > 0 ? text[(charLocation + 1)..] : text;
    }

    #endregion Text
     
    #region Clear Special Characters

    /// <summary>
    /// Remove caracteres acentuados de uma string, substituindo-os por suas versões não acentuadas.
    /// </summary>
    /// <param name="value">A string a ser processada.</param>
    /// <returns>A string resultante após a remoção dos caracteres acentuados.</returns>
    public static string ClearAccentedCharacters(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value.Length < 1)
            return value;

        string result = value;
        string[] accented = ["ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û"];
        string[] nonAccented = ["c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U"];

        for (int i = 0; i < accented.Length; i++)
            result = result.Replace(accented[i], nonAccented[i]);

        return result;
    }

    /// <summary>
    /// Remove símbolos especiais de uma string, opcionalmente substituindo-os por espaços.
    /// </summary>
    /// <param name="value">A string a ser processada.</param>
    /// <param name="useSpace">Indica se os símbolos devem ser substituídos por espaços (true) ou removidos completamente (false).</param>
    /// <returns>A string resultante após a remoção dos símbolos especiais.</returns>
    public static string ClearSymbols(this string value, bool useSpace = false)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value.Length < 1)
            return value;

        if (useSpace)
            return ClearSymbolsRegex().Replace(value, " ").Replace("}", " ").Replace("{", " ").Replace("|", " ").Replace(",", " ").Replace("~", " ");
        else
            return ClearSymbolsRegex().Replace(value, "").Replace("}", "").Replace("{", "").Replace("|", "").Replace(",", "").Replace("~", "");
    }

    /// <summary>
    /// Remove caracteres especiais, incluindo caracteres acentuados e símbolos, de uma string, opcionalmente substituindo-os por espaços.
    /// </summary>
    /// <param name="value">A string a ser processada.</param>
    /// <param name="useSpace">Indica se os caracteres especiais devem ser substituídos por espaços (true) ou removidos completamente (false).</param>
    /// <returns>A string resultante após a remoção dos caracteres especiais.</returns>
    public static string ClearSpecialCharacters(this string value, bool useSpace = false)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value.Length < 1)
            return value;

        string result = value.ClearAccentedCharacters();
        return result.ClearSymbols(useSpace);
    }

    [GeneratedRegex(@"[^0-9A-Za-za-çÇáéíóúýÁÉÍÓÚÝàèìòùÀÈÌÒÙãõñäëïöüÿÄËÏÖÜÃÕÑâêîôûÂÊÎÔÛ ,]")]
    private static partial Regex ClearSymbolsRegex();

    #endregion Clear Special Characters

    #region Generic Conversions 

    /// <summary>
    /// Converte uma string para um array de bytes utilizando a codificação especificada.
    /// </summary>
    /// <param name="value">A string a ser convertida.</param>
    /// <param name="encode">A codificação a ser utilizada (padrão: UTF-8).</param>
    /// <returns>Um array de bytes representando a string fornecida na codificação especificada.</returns>
    public static byte[] StringToByteArray(this string value, TextEncode encode = TextEncode.UTF8)
    {
        ArgumentNullException.ThrowIfNull(value);

        return encode switch
        {
            TextEncode.ASCII => Encoding.ASCII.GetBytes(value),
            TextEncode.UTF8 => Encoding.UTF8.GetBytes(value),
            TextEncode.UTF16 => Encoding.BigEndianUnicode.GetBytes(value),
            TextEncode.UTF32 => Encoding.UTF32.GetBytes(value),
            TextEncode.Unicode => Encoding.Unicode.GetBytes(value),
            TextEncode.Latin1 => Encoding.Latin1.GetBytes(value),
            _ => Encoding.UTF8.GetBytes(value),
        };
    }

    /// <summary>
    /// Converte um array de bytes para uma string utilizando a codificação especificada.
    /// </summary>
    /// <param name="value">O array de bytes a ser convertido.</param>
    /// <param name="encode">A codificação a ser utilizada (padrão: UTF-8).</param>
    /// <returns>Uma string representando os bytes fornecidos na codificação especificada.</returns>
    public static string ByteArrayToString(this byte[] value, TextEncode encode = TextEncode.UTF8)
    {
        ArgumentNullException.ThrowIfNull(value);

        return encode switch
        {
            TextEncode.ASCII => Encoding.ASCII.GetString(value),
            TextEncode.UTF8 => Encoding.UTF8.GetString(value),
            TextEncode.UTF16 => Encoding.BigEndianUnicode.GetString(value),
            TextEncode.UTF32 => Encoding.UTF32.GetString(value),
            TextEncode.Unicode => Encoding.Unicode.GetString(value),
            TextEncode.Latin1 => Encoding.Latin1.GetString(value),
            _ => Encoding.UTF8.GetString(value),
        };
    }

    #endregion Generic Conversions
}
