namespace SimpleJobs.UnitaryTests.EntityFrameworkUtility.Setup;

public class CourseRepository(DbContextInMemory dbContext) : RepositoryBase<CourseEntity>(dbContext)
{
}