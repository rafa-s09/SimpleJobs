using SimpleJobs.Repository;

namespace SimpleJobs.UnitaryTests.Repository.Setup
{
    public class CourseRepository : RepositoryBase<CourseEntity>
    {
        public CourseRepository(DbContextInMemory dbContext) : base(dbContext)
        {
        }
    }
}