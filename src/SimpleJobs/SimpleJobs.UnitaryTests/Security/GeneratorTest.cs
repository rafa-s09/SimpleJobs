namespace SimpleJobs.UnitaryTests.Security;

public class GeneratorTests
{
    [Theory]
    [InlineData(10, false)]
    [InlineData(15, true)]
    public void RandomText_LengthAndSpecialCharacters_Success(int length, bool includeSpecialCharacters)
    {
        string randomText = Generator.RandomText(length, includeSpecialCharacters);

        Assert.Equal(length, randomText.Length);
        if (!includeSpecialCharacters)        
            Assert.DoesNotContain("!@#$%^&*", randomText);
        
    }

    [Fact]
    public void Uid_Format_Success()
    {
        string uid = Generator.Uid();

        Assert.Equal(20, uid.Length); // 3 + 5 + 5 + 4 + 3 hyphens
        Assert.Contains("-", uid, StringComparison.Ordinal);
    }

    [Fact]
    public void DefaultGuid_WithSpecialCharacters_Success()
    {
        string defaultGuid = Generator.DefaultGuid(includeSpecialCharacters: true);

        Assert.Equal(36, defaultGuid.Length); // Including hyphens
        Assert.Contains("-", defaultGuid, StringComparison.Ordinal);
    }

    [Fact]
    public void DefaultGuid_WithoutSpecialCharacters_Success()
    {
        string defaultGuid = Generator.DefaultGuid(includeSpecialCharacters: false);

        Assert.Equal(32, defaultGuid.Length); // No hyphens
        Assert.DoesNotContain("-", defaultGuid);
    }
}