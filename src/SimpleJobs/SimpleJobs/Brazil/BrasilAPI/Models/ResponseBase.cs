namespace SimpleJobs.Brazil.BrasilAPI;

/// <summary>
/// Default BrasilAPI Response
/// </summary>
/// <typeparam name="TEntity">Response Content Model</typeparam>
public class ResponseBase<TEntity> : IResponseBase<TEntity> where TEntity : class
{
    /// <summary>
    /// Success status
    /// </summary>
    public bool Success { get; set; } = false;

    /// <summary>
    /// Result mensage
    /// </summary>
    public string Mensage { get; set; } = string.Empty;

    /// <summary>
    /// Result Content
    /// </summary>
    public TEntity? Content { get; set; } = null;
}
