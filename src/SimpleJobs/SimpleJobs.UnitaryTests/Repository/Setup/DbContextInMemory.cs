namespace SimpleJobs.UnitaryTests.Repository.Setup;

public class DbContextInMemory : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    public DbSet<CourseEntity> Courses { get; set; }
}