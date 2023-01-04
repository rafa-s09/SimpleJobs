namespace SimpleJobs.Repository;

/// <summary>
/// Adds an abstraction layer at the top of the query layer and helps eliminate duplicate logic in the implementation of your query code to the entity model
/// <br /> <i>NOTE: All functions commit automatically</i>
/// </summary>
/// <typeparam name="TEntity">Target Entity</typeparam>
public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
{
#pragma warning disable CS8602 // Dereference of a possibly null reference

    #region Constructor

    /// <summary>
    /// DbContext represents a session with the database which can be used to query and save instances of your entities to a database
    /// </summary>
    private readonly DbContext? _context;

    /// <summary>
    /// Class Constructor 
    /// </summary>
    /// <param name="dbContext"><b>dbContext</b> represents a session with the database which can be used to query and save instances of your entities to a database</param>
    public RepositoryBase(DbContext dbContext)
    {
        _context = dbContext;
    }

    #endregion Constructor

    #region Disposable

    /// <summary>
    /// Class Deconstructor 
    /// </summary>  
    ~RepositoryBase() => Dispose();

    /// <summary>
    /// Discard unmanaged resources and suppress checkout
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Protected implementation of the Dispose standard
    /// </summary>
    /// <param name="disposing">Dispose the stored value [Default is false]</param>
    protected virtual void Dispose(bool disposing = false)
    {
        /// Managed objects
        if (disposing)
            _context.Dispose();
    }

    #endregion Disposable

    #region Sync

    /// <summary>
    /// Insert the new data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public void Insert(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    /// <summary>
    /// Batch the new data into the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public void BatchInsert(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
        _context.SaveChanges();
    }

    /// <summary>
    /// Updates the data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    /// <summary>
    /// Updates the list of data in the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public void BatchUpdate(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Removes data from the table by id
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity</returns>
    public void DeleteById(params object[] id)
    {
        TEntity? obj = _context.Set<TEntity>().Find(id);
        if (obj != null)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Removes data from the table
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    public void Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    /// <summary>
    /// Removes the data list from the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    /// <returns>List of entities</returns>
    public void BatchDelete(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).ToList();
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IQueryable</returns>
    public IQueryable<TEntity> QueryableSearch(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true));
    }

    /// <summary>
    /// Performs the data search in the table and order the result
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="sortExpression">Lambda expression</param>
    /// <param name="ascendant">Ascending order if true [default is true]</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> SortedSearch(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, bool>> sortExpression, bool ascendant = true)
    {
        if (ascendant)
            return _context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true)).ToList();
        else
            return _context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true)).ToList();
    }

    /// <summary>
    /// Performs the data search in the table and order the result
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="sortExpression">Lambda expression</param>
    /// <param name="ascendant">Ascending order if true [default is true]</param>
    /// <returns>List of entities as IQueryable</returns>
    public IQueryable<TEntity> SortedQueryableSearch(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, bool>> sortExpression, bool ascendant = true)
    {
        if (ascendant)
            return _context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true));
        else
            return _context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true));
    }

    /// <summary>
    /// Returns all data from the table
    /// </summary>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    /// <summary>
    /// Returns the table data that the id was entered
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity or Null</returns>
    public TEntity? GetById(params object[] id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public TEntity? GetFirst()
    {
        return _context.Set<TEntity>().FirstOrDefault();
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public TEntity? GetFirst(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public TEntity? GetLast()
    {
        return _context.Set<TEntity>().LastOrDefault();
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public TEntity? GetLast(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().LastOrDefault(expression ?? (x => true));
    }

    /// <summary>
    /// Performs data search in the table and returns the desired quantity, skipping a certain quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> GetSome(Expression<Func<TEntity, bool>> expression, int skip, int amount)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount).ToList();
    }

    /// <summary>
    /// Performs data search on the table and skips a certain amount
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> Skip(Expression<Func<TEntity, bool>> expression, int skip)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).ToList();
    }

    /// <summary>
    /// Perform data search on the table and return the desired quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> Take(Expression<Func<TEntity, bool>> expression, int amount)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount).ToList();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <returns>Counted quantity</returns>
    public int Count()
    {
        return _context.Set<TEntity>().Count();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Counted quantity</returns>
    public int Count(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Count(expression ?? (x => true));
    }

    /// <summary>
    /// Checks if a data exists <br/>
    /// <i>Note: if the expression returns more than one result, it will be considered to exist</i>
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>If it exists, returns true</returns>
    public bool Exist(Expression<Func<TEntity, bool>> expression)
    {
        var entity = _context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
        if (entity != null)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks if a data exists
    /// </summary>
    /// <param name="id">Target entity Id</param>
    /// <returns>If it exists, returns true</returns>
    public bool Exist(params object[] id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        if (entity != null)
            return true;
        else
            return false;
    }

    #endregion Sync

    #region ASync

    /// <summary>
    /// Insert the new data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Batch the new data into the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public async Task BatchInsertAsync(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Updates the data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the list of data in the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public async Task BatchUpdateAsync(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Removes data from the table by id
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity</returns>
    public async Task DeleteByIdAsync(params object[] id)
    {
        TEntity? obj = _context.Set<TEntity>().Find(id);
        if (obj != null)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Removes data from the table
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    public async Task DeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes the data list from the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    /// <returns>List of entities</returns>
    public async Task BatchDeleteAsync(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).ToListAsync();
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IQueryable</returns>
    public async Task<IQueryable<TEntity>> QueryableSearchAsync(Expression<Func<TEntity, bool>> expression)
    {
        IQueryable<TEntity> entities = _context.Set<TEntity>().Where(expression ?? (x => true));
        await Task.CompletedTask;
        return entities;
    }

    /// <summary>
    /// Performs the data search in the table and order the result
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="sortExpression">Lambda expression</param>
    /// <param name="ascendant">Ascending order if true [default is true]</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> SortedSearchAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, bool>> sortExpression, bool ascendant = true)
    {
        if (ascendant)
            return await _context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true)).ToListAsync();
        else
            return await _context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true)).ToListAsync();
    }

    /// <summary>
    /// Performs the data search in the table and order the result
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="sortExpression">Lambda expression</param>
    /// <param name="ascendant">Ascending order if true [default is true]</param>
    /// <returns>List of entities as IQueryable</returns>
    public async Task<IQueryable<TEntity>> SortedQueryableSearchAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, bool>> sortExpression, bool ascendant = true)
    {
        IQueryable<TEntity> entities;
        if (ascendant)
            entities = _context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true));
        else
            entities = _context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true));

        await Task.CompletedTask;
        return entities;
    }

    /// <summary>
    /// Returns all data from the table
    /// </summary>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Returns the table data that the id was entered
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetByIdAsync(params object[] id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetFirstAsync()
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync();
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetLastasync()
    {
        return await _context.Set<TEntity>().LastOrDefaultAsync();
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetLastasync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().LastOrDefaultAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Performs data search in the table and returns the desired quantity, skipping a certain quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> GetSomeAsync(Expression<Func<TEntity, bool>> expression, int skip, int amount)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount).ToListAsync();
    }

    /// <summary>
    /// Performs data search on the table and skips a certain amount
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> SkipAsync(Expression<Func<TEntity, bool>> expression, int skip)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).ToListAsync();
    }

    /// <summary>
    /// Perform data search on the table and return the desired quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> TakeAsync(Expression<Func<TEntity, bool>> expression, int amount)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount).ToListAsync();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <returns>Counted quantity</returns>
    public async Task<int> CountAsync()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Counted quantity</returns>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().CountAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Checks if a data exists <br/>
    /// <i>Note: if the expression returns more than one result, it will be considered to exist</i>
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>If it exists, returns true</returns>
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression ?? (x => true));
        if (entity != null)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks if a data exists
    /// </summary>
    /// <param name="id">Target entity Id</param>
    /// <returns>If it exists, returns true</returns>
    public async Task<bool> ExistAsync(params object[] id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
            return true;
        else
            return false;
    }

    #endregion ASync

#pragma warning restore CS8602 // Dereference of a possibly null reference
}