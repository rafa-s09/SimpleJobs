namespace SimpleJobs.Entity;

/// <summary>
/// Adds an abstraction layer at the top of the query layer and helps eliminate duplicate logic in the implementation of your query code to the entity model
/// </summary>
/// <typeparam name="TEntity">Target Entity</typeparam>
public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
{
#pragma warning disable CS8602 // Dereference of a possibly null reference

    #region Constructor

    /// <summary>
    /// DbContext represents a session with the database which can be used to query and save instances of your entities to a database
    /// </summary>
    private DbContext? Context;

    /// <summary>
    /// Class Constructor 
    /// </summary>
    /// <param name="dbContext"><b>dbContext</b> represents a session with the database which can be used to query and save instances of your entities to a database</param>
    public Repository(DbContext dbContext)
    {
        Context = dbContext;
    }

    #endregion Constructor

    #region Disposable

    /// <summary>
    /// Class Deconstructor 
    /// </summary>  
    ~Repository() => Dispose();

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
            Context = null;        
    }

    #endregion Disposable

    #region Sync

    /// <summary>
    /// Insert the new data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public void Insert(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
        Context.SaveChanges();
    }

    /// <summary>
    /// Batch the new data into the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public void BatchInsert(IList<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }
    }

    /// <summary>
    /// Updates the data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public void Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
    }

    /// <summary>
    /// Updates the list of data in the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public void BatchUpdate(IList<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }

    /// <summary>
    /// Removes data from the table by id
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity</returns>
    public void DeleteById(params object[] id)
    {
        TEntity? obj = Context.Set<TEntity>().Find(id);
        if (obj != null)
        {
            Context.Entry(obj).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(obj);
            Context.SaveChanges();
        }
    }

    /// <summary>
    /// Removes data from the table
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    public void Delete(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        Context.Set<TEntity>().Remove(entity);
        Context.SaveChanges();
    }

    /// <summary>
    /// Removes the data list from the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    /// <returns>List of entities</returns>
    public void BatchDelete(IList<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
        }
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Where(expression ?? (x => true)).ToList();
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IQueryable</returns>
    public IQueryable<TEntity> QueryableSearch(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Where(expression ?? (x => true));
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
            return Context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true)).ToList();
        else
            return Context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true)).ToList();
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
            return Context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true));
        else
            return Context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true));
    }

    /// <summary>
    /// Returns all data from the table
    /// </summary>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    /// <summary>
    /// Returns the table data that the id was entered
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity or Null</returns>
    public TEntity? GetById(params object[] id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public TEntity? GetFirst()
    {
        return Context.Set<TEntity>().FirstOrDefault();
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public TEntity? GetFirst(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public TEntity? GetLast()
    {
        return Context.Set<TEntity>().LastOrDefault();
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public TEntity? GetLast(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().LastOrDefault(expression ?? (x => true));
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
        return Context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount).ToList();
    }

    /// <summary>
    /// Performs data search on the table and skips a certain amount
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> Skip(Expression<Func<TEntity, bool>> expression, int skip)
    {
        return Context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).ToList();
    }

    /// <summary>
    /// Perform data search on the table and return the desired quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    public IEnumerable<TEntity> Take(Expression<Func<TEntity, bool>> expression, int amount)
    {
        return Context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount).ToList();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <returns>Counted quantity</returns>
    public int Count()
    {
        return Context.Set<TEntity>().Count();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Counted quantity</returns>
    public int Count(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().Count(expression ?? (x => true));
    }

    /// <summary>
    /// Checks if a data exists <br/>
    /// <i>Note: if the expression returns more than one result, it will be considered to exist</i>
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>If it exists, returns true</returns>
    public bool Exist(Expression<Func<TEntity, bool>> expression)
    {
        var entity = Context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
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
        var entity = Context.Set<TEntity>().Find(id);
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
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Batch the new data into the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public async Task BatchInsertAsync(IList<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Updates the data in the table
    /// </summary>
    /// <param name="entity">Entity</param>
    public async Task UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates the list of data in the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    public async Task BatchUpdateAsync(IList<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Removes data from the table by id
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity</returns>
    public async Task DeleteByIdAsync(params object[] id)
    {
        TEntity? obj = Context.Set<TEntity>().Find(id);
        if (obj != null)
        {
            Context.Entry(obj).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(obj);
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Removes data from the table
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Entity</returns>
    public async Task DeleteAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes the data list from the table
    /// </summary>
    /// <param name="entities">List of entities</param>
    /// <returns>List of entities</returns>
    public async Task BatchDeleteAsync(IList<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().Where(expression ?? (x => true)).ToListAsync();
    }

    /// <summary>
    /// Performs the data search in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>List of entities as IQueryable</returns>
    public async Task<IQueryable<TEntity>> QueryableSearchAsync(Expression<Func<TEntity, bool>> expression)
    {
        IQueryable<TEntity> entities = Context.Set<TEntity>().Where(expression ?? (x => true));
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
            return await Context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true)).ToListAsync();
        else
            return await Context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true)).ToListAsync();
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
            entities = Context.Set<TEntity>().Where(expression ?? (x => true)).OrderBy(sortExpression ?? (x => true));
        else
            entities = Context.Set<TEntity>().Where(expression ?? (x => true)).OrderByDescending(sortExpression ?? (x => true));

        await Task.CompletedTask;
        return entities;
    }

    /// <summary>
    /// Returns all data from the table
    /// </summary>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Returns the table data that the id was entered
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetByIdAsync(params object[] id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetFirstAsync()
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync();
    }

    /// <summary>
    /// Returns the first data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetLastasync()
    {
        return await Context.Set<TEntity>().LastOrDefaultAsync();
    }

    /// <summary>
    /// Returns the last data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Entity or Null</returns>
    public async Task<TEntity?> GetLastasync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().LastOrDefaultAsync(expression ?? (x => true));
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
        return await Context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount).ToListAsync();
    }

    /// <summary>
    /// Performs data search on the table and skips a certain amount
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="skip">Skip amount</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> SkipAsync(Expression<Func<TEntity, bool>> expression, int skip)
    {
        return await Context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).ToListAsync();
    }

    /// <summary>
    /// Perform data search on the table and return the desired quantity
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <param name="amount">Desired quantity</param>
    /// <returns>List of entities as IEnumerable</returns>
    public async Task<IEnumerable<TEntity>> TakeAsync(Expression<Func<TEntity, bool>> expression, int amount)
    {
        return await Context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount).ToListAsync();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <returns>Counted quantity</returns>
    public async Task<int> CountAsync()
    {
        return await Context.Set<TEntity>().CountAsync();
    }

    /// <summary>
    /// Counts the amount of data in the table
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>Counted quantity</returns>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().CountAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Checks if a data exists <br/>
    /// <i>Note: if the expression returns more than one result, it will be considered to exist</i>
    /// </summary>
    /// <param name="expression">Lambda expression</param>
    /// <returns>If it exists, returns true</returns>
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(expression ?? (x => true));
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
        var entity = await Context.Set<TEntity>().FindAsync(id);
        if (entity != null)
            return true;
        else
            return false;
    }

    #endregion ASync

#pragma warning restore CS8602 // Dereference of a possibly null reference
}