namespace SimpleJobs.EntityFrameworkUtility;

/// <summary>
/// Representa uma classe base genérica para repositórios no Entity Framework.
/// </summary>
/// <typeparam name="TEntity">O tipo da entidade gerenciada pelo repositório.</typeparam>
public class RepositoryBase<TEntity>(DbContext dbContext) : IDisposable where TEntity : class
{
    #region Private

    /// <summary>
    /// Instância do DbContext utilizada pelo repositório.
    /// </summary>
    private readonly DbContext _context = dbContext;

    #endregion Private

    #region Disposable

    /// <summary>
    /// Destrutor da instância da classe <see cref="RepositoryBase{TEntity}"/>.
    /// </summary>
    ~RepositoryBase() => Dispose();

    /// <summary>
    /// Libera os recursos não gerenciados usados pelo <see cref="RepositoryBase{TEntity}"/>
    /// e, opcionalmente, libera os recursos gerenciados.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion Disposable

    #region Sync

    /// <summary>
    /// Insere uma entidade no contexto e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser inserida.</param>
    public void Insert(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    /// <summary>
    /// Insere uma coleção de entidades no contexto e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entities">A coleção de entidades a serem inseridas em lote.</param>
    public void BatchInsert(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
        _context.SaveChanges();
    }

    /// <summary>
    /// Atualiza o estado de uma entidade para modificado e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser atualizada.</param>
    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    /// <summary>
    /// Atualiza o estado de uma coleção de entidades para modificado e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entities">A coleção de entidades a ser atualizada em lote.</param>
    public void BatchUpdate(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Exclui uma entidade com base em sua chave primária e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="id">Os valores da chave primária da entidade a ser excluída.</param>
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
    /// Exclui uma entidade, define seu estado como excluído e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser excluída.</param>
    public void Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    /// <summary>
    /// Exclui uma coleção de entidades, define o estado delas como excluídas e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entities">A coleção de entidades a ser excluída em lote.</param>
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
    /// Executa uma busca no contexto com base em uma expressão de filtro e retorna uma coleção de entidades que atendem aos critérios.
    /// Se a expressão for nula, retorna todas as entidades.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro.</returns>
    public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).ToList();
    }

    /// <summary>
    /// Cria uma consulta no contexto com base em uma expressão de filtro.
    /// Se a expressão for nula, retorna todas as entidades.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a consulta.</param>
    /// <returns>Uma consulta IQueryable de entidades que atendem aos critérios da expressão de filtro.</returns>
    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true));
    }

    // <summary>
    /// Retorna todas as entidades no contexto.
    /// </summary>
    /// <returns>Uma coleção de todas as entidades no contexto.</returns>
    public IEnumerable<TEntity> All()
    {
        return _context.Set<TEntity>().ToList();
    }

    /// <summary>
    /// Retorna uma entidade do contexto com base em sua chave primária.
    /// </summary>
    /// <param name="id">Os valores da chave primária da entidade a ser obtida.</param>
    /// <returns>A entidade correspondente à chave primária fornecida ou null se não for encontrada.</returns>
    public TEntity? GetById(params object[] id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    /// <summary>
    /// Retorna a primeira entidade no contexto, ou null se não houver entidades.
    /// </summary>
    /// <returns>A primeira entidade no contexto ou null se não houver entidades.</returns>
    public TEntity? First()
    {
        return _context.Set<TEntity>().FirstOrDefault();
    }

    /// <summary>
    /// Retorna a primeira entidade no contexto que atenda aos critérios especificados pela expressão de filtro, ou null se não houver entidades que atendam aos critérios.
    /// Se a expressão for nula, retorna a primeira entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca da primeira entidade.</param>
    /// <returns>A primeira entidade no contexto que atende aos critérios da expressão de filtro ou null se não houver entidades que atendam aos critérios.</returns>
    public TEntity? First(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
    }

    /// <summary>
    /// Retorna a última entidade no contexto, ou null se não houver entidades.
    /// </summary>
    /// <returns>A última entidade no contexto ou null se não houver entidades.</returns>
    public TEntity? Last()
    {
        return _context.Set<TEntity>().LastOrDefault();
    }

    /// <summary>
    /// Retorna a última entidade no contexto que atenda aos critérios especificados pela expressão de filtro, ou null se não houver entidades que atendam aos critérios.
    /// Se a expressão for nula, retorna a última entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca da última entidade.</param>
    /// <returns>A última entidade no contexto que atende aos critérios da expressão de filtro ou null se não houver entidades que atendam aos critérios.</returns>
    public TEntity? Last(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().LastOrDefault(expression ?? (x => true));
    }

    /// <summary>
    /// Retorna uma quantidade específica de entidades no contexto que atendam aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, retorna a quantidade especificada de entidades no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca das entidades.</param>
    /// <param name="skip">O número de entidades a serem ignoradas no início.</param>
    /// <param name="amount">A quantidade de entidades a serem retornadas.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro.</returns>
    public IEnumerable<TEntity> Some(Expression<Func<TEntity, bool>> expression, int skip, int amount)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount).ToList();
    }

    /// <summary>
    /// Retorna uma coleção de entidades no contexto, ignorando um número específico de entidades no início.
    /// Se a expressão for nula, retorna uma coleção de todas as entidades no contexto, ignorando o número especificado no início.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca das entidades.</param>
    /// <param name="skip">O número de entidades a serem ignoradas no início.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro, ignorando o número especificado no início.</returns>
    public IEnumerable<TEntity> Skip(Expression<Func<TEntity, bool>> expression, int skip)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).ToList();
    }

    /// <summary>
    /// Retorna uma quantidade específica de entidades no contexto que atendam aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, retorna a quantidade especificada de entidades no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca das entidades.</param>
    /// <param name="amount">A quantidade de entidades a serem retornadas.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro, limitada à quantidade especificada.</returns>
    public IEnumerable<TEntity> Take(Expression<Func<TEntity, bool>> expression, int amount)
    {
        return _context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount).ToList();
    }

    /// <summary>
    /// Retorna o número total de entidades no contexto.
    /// </summary>
    /// <returns>O número total de entidades no contexto.</returns>
    public int Count()
    {
        return _context.Set<TEntity>().Count();
    }

    /// <summary>
    /// Retorna o número total de entidades no contexto que atendem aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, retorna o número total de entidades no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a contagem das entidades.</param>
    /// <returns>O número total de entidades no contexto que atendem aos critérios da expressão de filtro.</returns>
    public int Count(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Count(expression ?? (x => true));
    }

    /// <summary>
    /// Verifica se há pelo menos uma entidade no contexto que atenda aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, verifica se há pelo menos uma entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a verificação da existência da entidade.</param>
    /// <returns>True se houver pelo menos uma entidade que atenda aos critérios da expressão de filtro, false caso contrário.</returns>
    public bool Exist(Expression<Func<TEntity, bool>> expression)
    {
        var entity = _context.Set<TEntity>().FirstOrDefault(expression ?? (x => true));
        return entity != null;
    }

    /// <summary>
    /// Verifica se há uma entidade no contexto com base em sua chave primária.
    /// </summary>
    /// <param name="id">Os valores da chave primária da entidade a ser verificada.</param>
    /// <returns>True se houver uma entidade com a chave primária fornecida, false caso contrário.</returns>
    public bool Exist(params object[] id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        return entity != null;
    }

    /// <summary>
    /// Verifica se há alguma entidade no contexto que atende aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, verifica se há alguma entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a verificação.</param>
    /// <returns>True se houver pelo menos uma entidade que atenda aos critérios da expressão de filtro, false caso contrário.</returns>
    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Any(expression ?? (x => true));
    }

    #endregion Sync

    #region Async

    /// <summary>
    /// Assíncrono <br/>
    /// Insere uma entidade no contexto e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser inserida.</param>
    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Insere uma coleção de entidades no contexto e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entities">A coleção de entidades a serem inseridas em lote.</param>
    public async Task BatchInsertAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Atualiza o estado de uma entidade para modificado e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser atualizada.</param>
    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Atualiza o estado de uma coleção de entidades para modificado e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entities">A coleção de entidades a ser atualizada em lote.</param>
    public async Task BatchUpdateAsync(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Exclui uma entidade com base em sua chave primária e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="id">Os valores da chave primária da entidade a ser excluída.</param>
    public async Task DeleteByIdAsync(params object[] id)
    {
        TEntity? obj = await _context.Set<TEntity>().FindAsync(id);
        if (obj != null)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Exclui uma entidade, define seu estado como excluído e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser excluída.</param>
    public async Task DeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Exclui uma coleção de entidades, define o estado delas como excluídas e salva as alterações no banco de dados.
    /// </summary>
    /// <param name="entities">A coleção de entidades a ser excluída em lote.</param>
    public async Task BatchDeleteAsync(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(entity);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Executa uma busca no contexto com base em uma expressão de filtro e retorna uma coleção de entidades que atendem aos critérios.
    /// Se a expressão for nula, retorna todas as entidades.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro.</returns>
    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).ToListAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Cria uma consulta no contexto com base em uma expressão de filtro.
    /// Se a expressão for nula, retorna todas as entidades.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a consulta.</param>
    /// <returns>Uma consulta IQueryable de entidades que atendem aos critérios da expressão de filtro.</returns>
    public async Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> expression)
    {
        IQueryable<TEntity> entities = _context.Set<TEntity>().Where(expression ?? (x => true));
        await Task.CompletedTask;
        return entities;
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna todas as entidades no contexto.
    /// </summary>
    /// <returns>Uma coleção de todas as entidades no contexto.</returns>
    public async Task<IEnumerable<TEntity>> AllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna uma entidade do contexto com base em sua chave primária.
    /// </summary>
    /// <param name="id">Os valores da chave primária da entidade a ser obtida.</param>
    /// <returns>A entidade correspondente à chave primária fornecida ou null se não for encontrada.</returns>
    public async Task<TEntity?> GetByIdAsync(params object[] id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna a primeira entidade no contexto, ou null se não houver entidades.
    /// </summary>
    /// <returns>A primeira entidade no contexto ou null se não houver entidades.</returns>
    public async Task<TEntity?> FirstAsync()
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna a primeira entidade no contexto que atenda aos critérios especificados pela expressão de filtro, ou null se não houver entidades que atendam aos critérios.
    /// Se a expressão for nula, retorna a primeira entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca da primeira entidade.</param>
    /// <returns>A primeira entidade no contexto que atende aos critérios da expressão de filtro ou null se não houver entidades que atendam aos critérios.</returns>
    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna a última entidade no contexto, ou null se não houver entidades.
    /// </summary>
    /// <returns>A última entidade no contexto ou null se não houver entidades.</returns>
    public async Task<TEntity?> LastAsync()
    {
        return await _context.Set<TEntity>().LastOrDefaultAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna a última entidade no contexto que atenda aos critérios especificados pela expressão de filtro, ou null se não houver entidades que atendam aos critérios.
    /// Se a expressão for nula, retorna a última entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca da última entidade.</param>
    /// <returns>A última entidade no contexto que atende aos critérios da expressão de filtro ou null se não houver entidades que atendam aos critérios.</returns>
    public async Task<TEntity?> LastAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().LastOrDefaultAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna uma quantidade específica de entidades no contexto que atendam aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, retorna a quantidade especificada de entidades no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca das entidades.</param>
    /// <param name="skip">O número de entidades a serem ignoradas no início.</param>
    /// <param name="amount">A quantidade de entidades a serem retornadas.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro.</returns>
    public async Task<IEnumerable<TEntity>> SomeAsync(Expression<Func<TEntity, bool>> expression, int skip, int amount)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).Take(amount).ToListAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna uma coleção de entidades no contexto, ignorando um número específico de entidades no início.
    /// Se a expressão for nula, retorna uma coleção de todas as entidades no contexto, ignorando o número especificado no início.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca das entidades.</param>
    /// <param name="skip">O número de entidades a serem ignoradas no início.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro, ignorando o número especificado no início.</returns>
    public async Task<IEnumerable<TEntity>> SkipAsync(Expression<Func<TEntity, bool>> expression, int skip)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).Skip(skip).ToListAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna uma quantidade específica de entidades no contexto que atendam aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, retorna a quantidade especificada de entidades no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a busca das entidades.</param>
    /// <param name="amount">A quantidade de entidades a serem retornadas.</param>
    /// <returns>Uma coleção de entidades que atendem aos critérios da expressão de filtro, limitada à quantidade especificada.</returns>
    public async Task<IEnumerable<TEntity>> TakeAsync(Expression<Func<TEntity, bool>> expression, int amount)
    {
        return await _context.Set<TEntity>().Where(expression ?? (x => true)).Take(amount).ToListAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna o número total de entidades no contexto.
    /// </summary>
    /// <returns>O número total de entidades no contexto.</returns>
    public async Task<int> CountAsync()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Retorna o número total de entidades no contexto que atendem aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, retorna o número total de entidades no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a contagem das entidades.</param>
    /// <returns>O número total de entidades no contexto que atendem aos critérios da expressão de filtro.</returns>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().CountAsync(expression ?? (x => true));
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Verifica se há pelo menos uma entidade no contexto que atenda aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, verifica se há pelo menos uma entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a verificação da existência da entidade.</param>
    /// <returns>True se houver pelo menos uma entidade que atenda aos critérios da expressão de filtro, false caso contrário.</returns>
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression ?? (x => true));
        return entity != null;
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Verifica se há uma entidade no contexto com base em sua chave primária.
    /// </summary>
    /// <param name="id">Os valores da chave primária da entidade a ser verificada.</param>
    /// <returns>True se houver uma entidade com a chave primária fornecida, false caso contrário.</returns>
    public async Task<bool> ExistAsync(params object[] id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        return entity != null;
    }

    /// <summary>
    /// Assíncrono <br/>
    /// Verifica se há alguma entidade no contexto que atende aos critérios especificados pela expressão de filtro.
    /// Se a expressão for nula, verifica se há alguma entidade no contexto.
    /// </summary>
    /// <param name="expression">A expressão de filtro para a verificação.</param>
    /// <returns>True se houver pelo menos uma entidade que atenda aos critérios da expressão de filtro, false caso contrário.</returns>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().AnyAsync(expression ?? (x => true));
    }

    #endregion Async
}
