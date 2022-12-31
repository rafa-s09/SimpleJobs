using AutoFixture;

namespace SimpleJobs.UnitaryTests
{
    public abstract class BaseTest
    {
        protected Fixture Fixture { get; }

        public BaseTest()
        {
            Fixture = new Fixture();
        }
    }
}