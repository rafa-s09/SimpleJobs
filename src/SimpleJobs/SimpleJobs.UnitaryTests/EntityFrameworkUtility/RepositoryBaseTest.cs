

namespace SimpleJobs.UnitaryTests.EntityFrameworkUtility;

public class RepositoryBaseTest : TestBase, IDisposable
{
    private readonly DbContextInMemory _dbContext;
    private readonly RepositoryBase<CourseEntity> _repository;

    public RepositoryBaseTest()
    {
        _dbContext = new DbContextInMemory();
        _repository = new CourseRepository(_dbContext);
    }

    #region Disposable

    ~RepositoryBaseTest() => Dispose();

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
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        int result = _dbContext.Courses.Count();
        result.Should().BeGreaterThan(2);
        Assert.Equal(entities.Select(e => e.Name).ToList(), [.. _dbContext.Courses.Select(e => e.Name)]);
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
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];
        _repository.BatchInsert(entities);

        entities.ForEach(e => e.Name = "Updated");
        _repository.BatchUpdate(entities);

        Assert.Equal(entities.Select(e => e.Name).ToList(), [.. _dbContext.Courses.Select(e => e.Name)]);
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
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        _repository.Delete(entity);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public void BatchDelete_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        _repository.BatchDelete(entities);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public void Search_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        CourseEntity? result = _repository.Search(x => x.Name == "Mathematics").FirstOrDefault();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Where(x => x.Name == "Mathematics").First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void Query_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        CourseEntity? result = _repository.Query(x => x.Name == "Mathematics").FirstOrDefault();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Where(x => x.Name == "Mathematics").First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void All_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = _repository.All();
        result.Count().Should().Be(_dbContext.Courses.Count());
    }

    [Fact]
    public void GetById_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        Guid ID = _dbContext.Courses.First().Id;

        var result = _repository.GetById(ID);
        if (result != null)
            result.Should().Be(_dbContext.Courses.First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void First_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = _repository.First();
        if (result != null)
            result.Should().Be(_dbContext.Courses.First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void First_WithExpression_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = _repository.First(x => x.Name == "Science");
        if (result != null)
            result.Should().Be(_dbContext.Courses.First(x => x.Name == "Science"));
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void Last_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = _repository.Last();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Last());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void Last_WithExpression_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = _repository.Last(x => x.Name == "Science");
        if (result != null)
            result.Should().Be(_dbContext.Courses.Last(x => x.Name == "Science"));
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void Some_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = _repository.Some(x => x.Name.Contains('a'), 1, 1).First();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Where(x => x.Name.Contains('a')).Skip(1).Take(1).First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void Skip_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = _repository.Skip(x => x.Name.Contains('a'), 1).First();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Where(x => x.Name.Contains('a')).Skip(1).First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public void Take_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = _repository.Take(x => x.Name.Contains('a'), 1).First();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Where(x => x.Name.Contains('a')).Take(1).First());
        else
            Assert.Fail("Result can't be null!");
    }


    [Fact]
    public void Count_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = _repository.Count();
        result.Should().Be(_dbContext.Courses.Count());
    }

    [Fact]
    public void Count_WithExpression_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = _repository.Count(x => x.Name.Contains('a'));
        result.Should().Be(_dbContext.Courses.Count(x => x.Name.Contains('a')));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Exist_ById_FactTest(bool validId)
    {
        Guid ID;
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        if (validId)
            ID = _dbContext.Courses.First().Id;
        else
            ID = new Guid();

        var result = _repository.Exist(ID);
        result.Should().Be(validId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Exist_WithExpression_FactTest(bool validId)
    {
        bool result = false;
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        if (validId)
            result = _repository.Exist(x => x.Name == "Mathematics");
        else
            result = _repository.Exist(x => x.Name == "Geograph");

        result.Should().Be(validId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Any_FactTest(bool validId)
    {
        bool result = false;
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        if (validId)
            result = _repository.Any(x => x.Name == "Mathematics");
        else
            result = _repository.Any(x => x.Name == "Geograph");

        result.Should().Be(validId);
    }

    #endregion Sync

    #region ASync

    [Fact]
    public async void Async_Insert_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        await _repository.InsertAsync(entity);

        int result = _dbContext.Courses.Count();
        result.Should().BeGreaterThan(0);
        Assert.Equal(entity.Name, _dbContext.Courses.Select(e => e.Name).First());
    }

    [Fact]
    public async void Async_BatchInsert_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        await _repository.BatchInsertAsync(entities);

        int result = _dbContext.Courses.Count();
        result.Should().BeGreaterThan(2);
        Assert.Equal(entities.Select(e => e.Name).ToList(), [.. _dbContext.Courses.Select(e => e.Name)]);
    }

    [Fact]
    public async void Async_Update_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        entity.Name = "Calculus";
        await _repository.UpdateAsync(entity);

        Assert.Equal("Calculus", _dbContext.Courses.Single().Name);
    }

    [Fact]
    public async void Async_BatchUpdate_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];
        _repository.BatchInsert(entities);

        entities.ForEach(e => e.Name = "Updated");
        await _repository.BatchUpdateAsync(entities);

        Assert.Equal(entities.Select(e => e.Name).ToList(), [.. _dbContext.Courses.Select(e => e.Name)]);
    }

    [Fact]
    public async void Async_DeleteById_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        Guid IdToDelete = entity.Id;

        await _repository.DeleteByIdAsync(IdToDelete);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public async void Async_Delete_FactTest()
    {
        CourseEntity entity = new() { Id = Guid.NewGuid(), Name = "Mathematics" };

        _repository.Insert(entity);

        await _repository.DeleteAsync(entity);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public async void Async_BatchDelete_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        await _repository.BatchDeleteAsync(entities);

        int result = _dbContext.Courses.Count();
        result.Should().Be(0);
    }

    [Fact]
    public async void Async_Search_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.SearchAsync(x => x.Name == "Mathematics");
        if (result != null)
            result.First().Should().Be(_dbContext.Courses.Where(x => x.Name == "Mathematics").First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_Query_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.QueryAsync(x => x.Name == "Mathematics");
        if (result != null)
            result.First().Should().Be(_dbContext.Courses.Where(x => x.Name == "Mathematics").First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_All_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.AllAsync();
        result.Count().Should().Be(_dbContext.Courses.Count());
    }

    [Fact]
    public async void Async_GetById_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        Guid ID = _dbContext.Courses.First().Id;

        var result = await _repository.GetByIdAsync(ID);
        if (result != null)
            result.Should().Be(_dbContext.Courses.First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_First_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.FirstAsync();
        if (result != null)
            result.Should().Be(_dbContext.Courses.First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_First_WithExpression_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.FirstAsync(x => x.Name == "Science");
        if (result != null)
            result.Should().Be(_dbContext.Courses.First(x => x.Name == "Science"));
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_Last_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.LastAsync();
        if (result != null)
            result.Should().Be(_dbContext.Courses.Last());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_Last_WithExpression_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        var result = await _repository.LastAsync(x => x.Name == "Science");
        if (result != null)
            result.Should().Be(_dbContext.Courses.Last(x => x.Name == "Science"));
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_Some_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = await _repository.SomeAsync(x => x.Name.Contains('a'), 1, 1);
        if (result != null)
            result.First().Should().Be(_dbContext.Courses.Where(x => x.Name.Contains('a')).Skip(1).Take(1).First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_Skip_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = await _repository.SkipAsync(x => x.Name.Contains('a'), 1);
        if (result != null)
            result.First().Should().Be(_dbContext.Courses.Where(x => x.Name.Contains('a')).Skip(1).First());
        else
            Assert.Fail("Result can't be null!");
    }

    [Fact]
    public async void Async_Take_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = await _repository.TakeAsync(x => x.Name.Contains('a'), 1);
        if (result != null)
            result.First().Should().Be(_dbContext.Courses.Where(x => x.Name.Contains('a')).Take(1).First());
        else
            Assert.Fail("Result can't be null!");
    }


    [Fact]
    public async void Async_Count_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = await _repository.CountAsync();
        result.Should().Be(_dbContext.Courses.Count());
    }

    [Fact]
    public async void Async_Count_WithExpression_FactTest()
    {
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        var result = await _repository.CountAsync(x => x.Name.Contains('a'));
        result.Should().Be(_dbContext.Courses.Count(x => x.Name.Contains('a')));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void Async_Exist_ById_FactTest(bool validId)
    {
        Guid ID;
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }, new() { Id = Guid.NewGuid(), Name = "Geograph" }];

        _repository.BatchInsert(entities);

        if (validId)
            ID = _dbContext.Courses.First().Id;
        else
            ID = new Guid();

        var result = await _repository.ExistAsync(ID);
        result.Should().Be(validId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void Async_Exist_WithExpression_FactTest(bool validId)
    {
        bool result = false;
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        if (validId)
            result = await _repository.ExistAsync(x => x.Name == "Mathematics");
        else
            result = await _repository.ExistAsync(x => x.Name == "Geograph");

        result.Should().Be(validId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void Async_Any_FactTest(bool validId)
    {
        bool result = false;
        List<CourseEntity> entities = [new() { Id = Guid.NewGuid(), Name = "Mathematics" }, new() { Id = Guid.NewGuid(), Name = "Science" }, new() { Id = Guid.NewGuid(), Name = "History" }];

        _repository.BatchInsert(entities);

        if (validId)
            result = await _repository.AnyAsync(x => x.Name == "Mathematics");
        else
            result = await _repository.AnyAsync(x => x.Name == "Geograph");

        result.Should().Be(validId);
    }

    #endregion ASync


}