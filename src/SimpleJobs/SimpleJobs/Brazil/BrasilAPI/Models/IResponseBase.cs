namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Default BrasilAPI Response
/// </summary>
/// <typeparam name="TEntity">Response Content Model</typeparam>
public interface IResponseBase<TEntity> where TEntity : class
{
    /// <summary>
    /// Success status
    /// </summary>
    bool Success { get; set; }

    /// <summary>
    /// Result mensage
    /// </summary>
    string Mensage { get; set; }

    /// <summary>
    /// Result Content
    /// </summary>
    TEntity? Content { get; set; }
}

