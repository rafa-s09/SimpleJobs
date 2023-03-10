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

    [Fact]
    public void Insert_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        int result = _dbContext.Courses.Count();
        result.Should().BeGreaterThan(0);
        Assert.Equal(entity.Name, _dbContext.Courses.Select(e => e.Name).First());
    }

    [Fact]
    public void BatchInsert_FactTest()
    {
        List<CourseEntity> entities = new() { new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" } };

        _repository.BatchInsert(entities);

        int result = _dbContext.Courses.Count();
        result.Should().BeGreaterThan(2);
        Assert.Equal(entities.Select(e => e.Name).ToList(), _dbContext.Courses.Select(e => e.Name).ToList());
    }

    [Fact]
    public void Update_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        entity.Name = "Calculus";
        _repository.Update(entity);

        Assert.Equal("Calculus", _dbContext.Courses.Single().Name);
    }

    [Fact]
    public void BatchUpdate_FactTest()
    {
        List<CourseEntity> entities = new() { new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" } };
        _repository.BatchInsert(entities);

        entities.ForEach(e => e.Name = "Updated");
        _repository.BatchUpdate(entities);

        Assert.Equal(entities.Select(e => e.Name).ToList(), _dbContext.Courses.Select(e => e.Name).ToList());
    }

    [Fact]
    public void DeleteById_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        Guid IdToDelete = entity.Id;

        _repository.DeleteById(IdToDelete);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public void Delete_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics"};
        
        _repository.Insert(entity);

        _repository.Delete(entity);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public void BatchDelete_FactTest()
    {
        List<CourseEntity> entities = new() { new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" } };

        _repository.BatchInsert(entities);

        _repository.BatchDelete(entities);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }



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