namespace SimpleJobs.UnitaryTests.EntityFrameworkUtility.Setup;

public class DbContextInMemory : DbContext
{
    // Requer o Microsoft.EntityFrameworkCore.InMemory
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    public DbSet<CourseEntity> Courses { get; set; }
}