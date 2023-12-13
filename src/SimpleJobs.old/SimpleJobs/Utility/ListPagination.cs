namespace SimpleJobs.Utility;

/// <summary>
/// Simples Pagination Method
/// </summary>
/// <typeparam name="TEntity">List Type</typeparam>
public class ListPagination<TEntity> : IListPagination<TEntity>
{
    /// <summary>
    /// [Internal] Item list
    /// <br/> <i>NOTE: This value need to be a <b>List type</b> variable</i>
    /// </summary>
    private readonly List<TEntity> _items;

    /// <summary>
    /// [Internal] Page size
    /// </summary>
    private readonly int _pageSize;

    /// <summary>
    /// create a virtual pagination list
    /// </summary>
    /// <param name="items">List Items</param>
    /// <param name="pageSize">Page size</param>
    public ListPagination(IEnumerable<TEntity> items, int pageSize)
    {
        _items = items.ToList();
        _pageSize = pageSize;
    }

    /// <summary>
    /// Return total of pages
    /// </summary>
    public int PageCount => (int)Math.Ceiling((decimal)_items.Count / _pageSize);   

    /// <summary>
    /// Return total of items
    /// </summary>
    public int Count => _items.Count;    

    /// <summary>
    /// Return itens by page index
    /// </summary>
    /// <param name="pageIndex">Page index</param>
    /// <returns>Items list</returns>
    public IEnumerable<TEntity> GetPage(int pageIndex)
    {
        int startIndex = pageIndex * _pageSize;
        int endIndex = Math.Min((pageIndex + 1) * _pageSize - 1, _items.Count - 1);

        return _items.GetRange(startIndex, endIndex - startIndex + 1);
    }

    /// <summary>
    /// Return itens of first page
    /// </summary>
    /// <returns>First page items list</returns>    
    public IEnumerable<TEntity> FirstPage() => GetPage(0);

    /// <summary>
    /// Return itens of last page
    /// </summary>
    /// <returns>Last page items list</returns>  
    public IEnumerable<TEntity> LastPage() => GetPage(_pageSize);
}



