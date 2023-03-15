namespace SimpleJobs.Repository;

/// <summary>
/// Adds an abstraction layer at the top of the query layer and helps eliminate duplicate logic in the implementation of your query code to the entity model
/// </summary>
/// <typeparam name="TEntity">Target Entity</typeparam>
public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    #region Sync

    /// <summary>
    /// Insert the new data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    void Insert(TEntity entity);

    /// <summary>
    /// Batch the new data into the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    void BatchInsert(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates the data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    void Update(TEntity entity);

    /// <summary>
    /// Updates the list of data in the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    void BatchUpdate(IEnumerable<TEntity> entities);

    /// <summary>
    /// Removes data from the table by id
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity</returns>
    void DeleteById(params object[] id);

    /// <summary>
    /// Removes data from the table
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    void Delete(TEntity entity);

    /// <summary>
    /// Removes the data list from the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    /// <returns>List of entities</returns>
    void BatchDelete(IEnumerable<TEntity> entities);

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IEnumerable</returns>
    IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IQueryable</returns>
    IQueryable<TEntity> QueryableSearch(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns all data from the table
    /// </summary>
    /// <returns>List of entities as IEnumerable</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Returns the table data that the id was entered
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity or Null</returns>
    TEntity? GetById(params object[] id);

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    TEntity? GetFirst();

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    TEntity? GetFirst(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    TEntity? GetLast();

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    TEntity? GetLast(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Performs data search in the table and returns the desired quantity, skipping a certain quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    IEnumerable<TEntity> GetSome(Expression<Func<TEntity, bool>> expression, int skip, int amount);

    /// <summary>
    /// Performs data search on the table and skips a certain amount
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <returns>List of entities as IEnumerable</returns>
    IEnumerable<TEntity> Skip(Expression<Func<TEntity, bool>> expression, int skip);

    /// <summary>
    /// Perform data search on the table and return the desired quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    IEnumerable<TEntity> Take(Expression<Func<TEntity, bool>> expression, int amount);

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <returns>Counted quantity</returns>
    int Count();

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Counted quantity</returns>
    int Count(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Checks if a data exists <br/>
    /// <i>Note: if the expression returns more than one result, it will be considered to exist</i>
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>If it exists, returns true</returns>
    bool Exist(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Checks if a data exists
    /// </summary>
    /// <param name="id">Target entity Id</param>
    /// <returns>If it exists, returns true</returns>
    bool Exist(params object[] id);

    #endregion Sync

    #region ASync

    /// <summary>
    /// Insert the new data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    Task InsertAsync(TEntity entity);

    /// <summary>
    /// Batch the new data into the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    Task BatchInsertAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates the data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Updates the list of data in the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    Task BatchUpdateAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Removes data from the table by id
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity</returns>
    Task DeleteByIdAsync(params object[] id);

    /// <summary>
    /// Removes data from the table
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Removes the data list from the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    /// <returns>List of entities</returns>
    Task BatchDeleteAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IEnumerable</returns>
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IQueryable</returns>
    Task<IQueryable<TEntity>> QueryableSearchAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns all data from the table
    /// </summary>
    /// <returns>List of entities as IEnumerable</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Returns the table data that the id was entered
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity or Null</returns>
    Task<TEntity?> GetByIdAsync(params object[] id);

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    Task<TEntity?> GetFirstAsync();

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    Task<TEntity?> GetLastAsync();

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    Task<TEntity?> GetLastAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Performs data search in the table and returns the desired quantity, skipping a certain quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    Task<IEnumerable<TEntity>> GetSomeAsync(Expression<Func<TEntity, bool>> expression, int skip, int amount);

    /// <summary>
    /// Performs data search on the table and skips a certain amount
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <returns>List of entities as IEnumerable</returns>
    Task<IEnumerable<TEntity>> SkipAsync(Expression<Func<TEntity, bool>> expression, int skip);

    /// <summary>
    /// Perform data search on the table and return the desired quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    Task<IEnumerable<TEntity>> TakeAsync(Expression<Func<TEntity, bool>> expression, int amount);

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <returns>Counted quantity</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Counted quantity</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Checks if a data exists <br/>
    /// <i>Note: if the expression returns more than one result, it will be considered to exist</i>
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>If it exists, returns true</returns>
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Checks if a data exists
    /// </summary>
    /// <param name="id">Target entity Id</param>
    /// <returns>If it exists, returns true</returns>
    Task<bool> ExistAsync(params object[] id);

    #endregion ASync
}

