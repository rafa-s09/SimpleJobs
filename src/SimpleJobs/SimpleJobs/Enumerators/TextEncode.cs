namespace SimpleJobs.Enumerators;

    /// <summary>
    /// Tipo de Encode de Texto
    /// </summary>
public enum TextEncode
{
    /// <summary>
    /// Encode ASCII
    /// </summary>
    ASCII = 0,
    /// <summary>
    /// Encode UTF8
    /// </summary>
    UTF8 = 1,
    /// <summary>
    /// Encode BigEndianUnicode (UTF16)
    /// </summary>
    UTF16 = 2,
    /// <summary>
    /// Encode UTF32
    /// </summary>
    UTF32 = 3,
    /// <summary>
    /// Encode Unicode
    /// </summary>
    Unicode = 4,
    /// <summary>
    /// Encode Latin1
    /// </summary>
    Latin1 = 5
}