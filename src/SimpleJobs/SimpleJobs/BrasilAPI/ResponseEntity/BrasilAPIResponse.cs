namespace SimpleJobs.BrasilAPI;

/// <summary>
/// Representa uma resposta genérica de API para a BrasilAPI.
/// </summary>
/// <typeparam name="TEntity">O tipo de conteúdo na resposta da API.</typeparam>
public record BrasilApiResponse<TEntity> where TEntity : class
{
    /// <summary>
    /// Informa se a solicitação da API foi bem-sucedida.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem associada à resposta da API.
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Conteúdo da resposta da API, pode ser null, se a solicitação da API não for bem-sucedida.
    /// </summary>
    public TEntity? Content { get; set; }
}
