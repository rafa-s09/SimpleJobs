namespace SimpleJobs.UnitaryTests.Repository;

public class RepositoryTest : BaseTest, IDisposable
{
    private readonly DbContextInMemory _dbContext;
    private readonly IRepository<CourseEntity> _repository;

    public RepositoryTest()
    {
        _dbContext = new DbContextInMemory();
        _repository = new CourseRepository(_dbContext);
    }

    #region Sync



    #endregion Sync

    #region ASync



    #endregion ASync

    #region Disposable

    ~RepositoryTest() => Dispose();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing = false)
    {
        /// Managed objects
        if (disposing)
            _repository.Dispose();
    }

    #endregion Disposable
}