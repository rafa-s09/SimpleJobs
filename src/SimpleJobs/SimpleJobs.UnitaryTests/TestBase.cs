namespace SimpleJobs.UnitaryTests;

public abstract class TestBase
{
    protected Fixture Fixture { get; }

    public TestBase() { Fixture = new Fixture(); }
}
