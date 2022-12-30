using AutoFixture;

namespace SimpleJobs.Test
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