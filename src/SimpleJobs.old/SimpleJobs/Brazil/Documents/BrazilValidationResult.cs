namespace SimpleJobs.Brazil.Documents;

/// <summary>
/// Default Response Enumerator for Brazilian document validation class
/// </summary>
public enum BrazilValidationResult
{
    /// <summary>
    /// Validation Successful
    /// </summary>
    Success = 0,

    /// <summary>
    /// Validation Failed
    /// </summary>
    Failed = 1,

    /// <summary>
    /// Wrong document size
    /// </summary>
    WrongSize = 2
}

