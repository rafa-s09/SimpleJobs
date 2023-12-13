namespace SimpleJobs.Enumerators;

/// <summary>
/// Resposta da validação de documento
/// </summary>
public enum DocumentValidationResponse
{
    /// <summary>
    /// Invalido
    /// </summary>
    Invalid = 0,
    /// <summary>
    /// Valido
    /// </summary>
    Valid = 1,
    /// <summary>
    /// Tamanho incorreto
    /// </summary>
    WrongSize = 2
}
