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

    }


    #endregion Sync

    #region ASync

    #endregion ASync

    public void Dispose()
    {
        _repository.Dispose();
    }
}