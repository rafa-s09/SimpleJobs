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
    public void Insert_InsertNewEntity_NotThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();

        Action act = () => _repository.Insert(entity);

        act.Should().NotThrow();
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Insert_InsertExistingEntity_ThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        _repository.Insert(entity);

        Action act = () => _repository.Insert(entity);

        var ex = act.Should().ThrowExactly<ArgumentException>();
        ex.WithMessage($"An item with the same key has already been added. Key: {entity.Id}");
    }

    [Fact]
    public void BatchInsert_InsertManyEntities_NotThrowException()
    {
        IEnumerable<CourseEntity> entities = Fixture.Build<CourseEntity>().Without(a => a.Id).CreateMany();

        Action act = () => _repository.BatchInsert(entities);

        act.Should().NotThrow();
        entities.Should().OnlyContain(a => a.Id != Guid.Empty);
    }

    [Fact]
    public void BatchInsert_InsertManyEntitiesWithAnExistingOne_ThrowException()
    {
        CourseEntity existingEntity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        CourseEntity entityNotInserted = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        IEnumerable<CourseEntity> entities = new List<CourseEntity>
        {
            Fixture.Build<CourseEntity>().Without(a => a.Id).Create(),
            existingEntity,
            entityNotInserted
        };
        _repository.Insert(existingEntity);

        Action act = () => _repository.BatchInsert(entities);

        var ex = act.Should().ThrowExactly<ArgumentException>();
        ex.WithMessage($"An item with the same key has already been added. Key: {existingEntity.Id}");

        IEnumerable<CourseEntity> insertedEntities = _repository.GetAll();
        insertedEntities.Should().HaveCount(2);
        insertedEntities.Should().NotContain(entityNotInserted);
    }

    [Fact]
    public void Update_UpdateEntity_NotThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();

        _repository.Insert(entity);

        entity.Name = "bbbb";

        Action act = () => _repository.Update(entity);

        act.Should().NotThrow();
        entity.Id.Should().NotBe(Guid.Empty);
        entity.Name.Should().Match("bbbb");
    }

    [Fact]
    public void Update_UpdateEntity_ThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        _repository.Insert(entity);

        entity.Id = new Guid();

        Action act = () => _repository.Update(entity);

        act.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void BatchUpdate_UpdateManyEntities_NotThrowException()
    {
        IEnumerable<CourseEntity> entities = Fixture.Build<CourseEntity>().Without(a => a.Id).CreateMany();
        _repository.BatchInsert(entities);
        List<CourseEntity> TempEntities = new();

        foreach (CourseEntity course in entities)
        {
            course.Name = "bbbb";
            TempEntities.Add(course);
        }

        Action act = () => _repository.BatchUpdate(TempEntities);
        act.Should().NotThrow();
    }

    [Fact]
    public void DeleteById_DeleteEntity_NotThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        _repository.Insert(entity);

        Action act = () => _repository.DeleteById(entity.Id);
        act.Should().NotThrow();
    }

    [Fact]
    public void DeleteById_DeleteEntity_ThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        _repository.Insert(entity);

        Action act = () => _repository.DeleteById("0");
        act.Should().ThrowExactly<ArgumentException>();
    }

    [Fact]
    public void Delete_DeleteEntity_NotThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        _repository.Insert(entity);

        Action act = () => _repository.Delete(entity);
        act.Should().NotThrow();
    }

    [Fact]
    public void Delete_DeleteEntity_ThrowException()
    {
        CourseEntity entity = Fixture.Build<CourseEntity>().Without(a => a.Id).Create();
        _repository.Insert(entity);

        Action act = () => _repository.Delete(new CourseEntity() { Id = new Guid(), Name = "bbbb"});
        act.Should().ThrowExactly<DbUpdateConcurrencyException>();
    }

    [Fact]
    public void BatchDelete_DeleteEntity_NotThrowException()
    {
        IEnumerable<CourseEntity> entities = Fixture.Build<CourseEntity>().Without(a => a.Id).CreateMany();
        _repository.BatchInsert(entities);       

        Action act = () => _repository.BatchDelete(entities);
        act.Should().NotThrow();
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