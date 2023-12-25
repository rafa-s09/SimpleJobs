namespace SimpleJobs.Utility;

/// <summary>
/// Classe que representa um objeto virtual com funcionalidades de paginação, filtragem, deleção,
/// atualização e recuperação de informações sobre a lista.
/// </summary>
public class VirtualList<TEntity>(List<TEntity> dataList, int size)
{
    /// <summary>
    /// Lista virtual
    /// </summary>
    private List<TEntity> virtualData = dataList;

    /// <summary>
    /// Tamanho da pagina
    /// </summary>
    private readonly int pageSize = size;

    /// <summary>
    /// Index da pagina
    /// </summary>
    private int page = 1;

    /// <summary>
    /// Obtém a lista atual após aplicar a paginação.
    /// </summary>
    public List<TEntity> PagedList
    {
        get { return virtualData.Skip((page - 1) * pageSize).Take(pageSize).ToList(); }
    }

    /// <summary>
    /// Realiza a paginação para a página especificada. <br/>
    /// Se menor que o minimo ira para a ultima pagina, se maior irá para primeira.
    /// </summary>
    /// <param name="pagina">O número da página desejada.</param>
    public void GoToPage(int target)
    {
        if (target < 1)
            page = PageCount;
        else if (target > PageCount)
            page = 1;
        else
            page = target;     
    }

    /// <summary>
    /// Filtra a lista com base em um predicado fornecido.
    /// </summary>
    /// <param name="predicado">O predicado de filtragem.</param>
    public void Filter(Func<TEntity, bool> predicado)
    {
        virtualData = virtualData.Where(predicado).ToList();
        page = 1; // Retorna à primeira página após a filtragem.
    }

    /// <summary>
    /// Obtém a quantidade total de itens na lista.
    /// </summary>
    public int ItensCount => virtualData.Count;

    /// <summary>
    /// Obtém o número total de páginas com base na quantidade de itens e itens por página.
    /// </summary>
    public int PageCount => (int)Math.Ceiling((double)ItensCount / pageSize);

    /// <summary>
    /// Obtém o índice da página atual.
    /// </summary>
    public int PageIndex => page;

    /// <summary>
    /// Obtém o índice do item especificado na lista completa.
    /// </summary>
    /// <param name="item">O item a ser localizado.</param>
    /// <returns>O índice do item ou -1 se não encontrado.</returns>
    public int GetItemIndex(TEntity item)
    {
        return virtualData.IndexOf(item);
    }
}
