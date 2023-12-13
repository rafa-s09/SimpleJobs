namespace SimpleJobs.Utility;

/// <summary>
/// Simples Pagination Method
/// </summary>
/// <typeparam name="TEntity">List Type</typeparam>
public interface IListPagination<TEntity>
{
    /// <summary>
    /// Return total of pages
    /// </summary>
    int PageCount { get; }

    /// <summary>
    /// Return total of items
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Return itens by page index
    /// </summary>
    /// <param name="pageIndex">Page index</param>
    /// <returns>Items list</returns>
    IEnumerable<TEntity> GetPage(int pageIndex);

    /// <summary>
    /// Return itens of first page
    /// </summary>
    /// <returns>First page items list</returns>    
    IEnumerable<TEntity> FirstPage();

    /// <summary>
    /// Return itens of last page
    /// </summary>
    /// <returns>Last page items list</returns>  
    IEnumerable<TEntity> LastPage();
}