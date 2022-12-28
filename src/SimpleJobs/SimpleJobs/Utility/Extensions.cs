namespace SimpleJobs.Utility;

/// <summary>
/// Contains a number of useful extensions to speed up development
/// </summary>
public static class Extensions
{
    #region Text      
    
    /// <summary>
    /// Return text up to the first character defined, or empty
    /// </summary>
    /// <param name="text">Input Text</param>
    /// <param name="stopAt">Char to Stop</param>
    /// <returns>Treated text or empty text</returns>
    public static string GetUntilOrEmpty(this string text, char stopAt)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);
                if (charLocation > 0)
                    return text[..charLocation];
            }
            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Return text up to the first character defined
    /// </summary>
    /// <param name="text">Input Text</param>
    /// <param name="stopAt">Char to Stop</param>
    /// <returns>Treated text or original text</returns>
    public static string GetUntil(this string text, char stopAt)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);
                if (charLocation > 0)
                    return text[..charLocation];
            }
            return text;
        }
        catch
        {
            return text;
        }
    }

    /// <summary>
    /// Return all text after first informed index or return empty
    /// </summary>
    /// <param name="text">Input text</param>
    /// <param name="startAt">Char to Start</param>
    /// <returns>Treated text or empty text</returns>
    public static string GetAfterOrEmpty(this string text, char startAt)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(startAt, StringComparison.Ordinal);
                if (charLocation > 0)
                    return text[charLocation..];
            }
            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Return all text after first informed index
    /// </summary>
    /// <param name="text">Input text</param>
    /// <param name="startAt">Char to Start</param>
    /// <returns>Treated text or original text</returns>
    public static string GetAfter(this string text, char startAt)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(startAt, StringComparison.Ordinal);
                if (charLocation > 0)
                    return text[charLocation..];
            }
            return text;
        }
        catch
        {
            return text;
        }
    }

    /// <summary>
    /// Exchanges accented characters with non-accented characters
    /// </summary>
    /// <param name="value">Input Text</param>
    /// <returns>Non-accented text</returns>
    public static string ClearAccentedCharacters(this string value)
    {
        string result = value;
        string[] accented = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
        string[] nonAccented = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

        for (int i = 0; i < accented.Length; i++)
            result = result.Replace(accented[i], nonAccented[i]);

        return result;
    }

    /// <summary>
    /// Remove symbols and dots
    /// </summary>
    /// <param name="value">Input Text</param>
    /// <returns>Clear text</returns>
    public static string ClearSymbols(this string value)
    {
        string result = value.TrimStart().TrimEnd();
        string[] symbols = new string[] { "-", "_", ".", ",", "\\", "/", "|", "~", "#", "$", "%", "&", "@", "\"", "'", "*", "=", "+", "ª", "º", ">", "<", ":", ";", "?", "!" };

        for (int i = 0; i < symbols.Length; i++)
            result = result.Replace(symbols[i], "");

        return result;
    }

    /// <summary>
    /// Exchanges accented characters with non-accented characters and remove symbols and dots
    /// </summary>
    /// <param name="value">Input Text</param>
    /// <returns>Clear text</returns>
    public static string ClearSpecialCharacters(this string value)
    {
        string result = value.ClearAccentedCharacters();
        return result.ClearSymbols();
    }

    #endregion Text

    #region Generic Conversions        
    
    /// <summary>
    /// Convert string to ByteArray
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="encode">Encode Format (Default is UTF8)</param>
    /// <returns>Byte Array Result</returns>
    public static byte[] StringToByteArray(this string value, TextEncode encode = TextEncode.UTF8)
    {
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
    /// Convert byte array to string
    /// </summary>
    /// <param name="value">Byte Array</param>
    /// <param name="encode">Encode Format (Default is UTF8)</param>
    /// <returns>String result</returns>
    public static string ByteArrayToString(this byte[] value, TextEncode encode = TextEncode.UTF8)
    {
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



