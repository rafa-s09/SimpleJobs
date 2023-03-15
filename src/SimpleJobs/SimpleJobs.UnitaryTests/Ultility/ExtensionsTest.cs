namespace SimpleJobs.UnitaryTests.Utility;

public class ExtensionsTest : BaseTest
{
    [Theory]
    [InlineData("abcdef", 'c', "ab")]
    [InlineData("abcdef", 'x', "")]
    [InlineData("", 'x', "")]
    [InlineData(null, 'x', "")]
    public void GetUntilOrEmpty_ReturnsExpectedResult(string input, char stopAt, string expectedOutput)
    {
        string result = input.GetUntilOrEmpty(stopAt);
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("abcdef", 'c', "ab")]
    [InlineData("abcdef", 'x', "abcdef")]
    [InlineData("", 'x', "")]
    [InlineData(null, 'x', null)]
    public void GetUntil_ReturnsExpectedResult(string input, char stopAt, string expectedOutput)
    {
        string result = input.GetUntil(stopAt);
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("abcdef", 'c', "def")]
    [InlineData("abcdef", 'x', "")]
    [InlineData("", 'x', "")]
    [InlineData(null, 'x', "")]
    public void GetAfterOrEmpty_ReturnsExpectedResult(string input, char startAt, string expectedOutput)
    {
        string result = input.GetAfterOrEmpty(startAt);
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("abcdef", 'c', "def")]
    [InlineData("abcdef", 'x', "abcdef")]
    [InlineData("", 'x', "")]
    [InlineData(null, 'x', null)]
    public void GetAfter_ReturnsExpectedResult(string input, char startAt, string expectedOutput)
    {
        string result = input.GetAfter(startAt);
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("Mëtàl Hëàd", "Metal Head")]
    [InlineData("Äbcdef", "Abcdef")]
    [InlineData("", "")]
    public void ClearAccentedCharacters_ReturnsExpectedResult(string input, string expectedOutput)
    {
        string result = input.ClearAccentedCharacters();
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(null)]
    public void ClearAccentedCharacters_ThrowException(string input)
    {
        Action act = () => input.ClearAccentedCharacters();
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData(@"abc!@#$%^&*()_+}{[]|:';<>,.?/~def", true, "abc                           def")]
    [InlineData(@"abc!@#$%^&*()_+}{[]|:';<>,.?/~def", false, "abcdef")]
    [InlineData("abcdef", true, "abcdef")]
    [InlineData("abcdef", false, "abcdef")]
    [InlineData("", true, "")]
    [InlineData("", false, "")]    
    public void ClearSymbols_ReturnsExpectedResult(string input, bool useSpace, string expectedOutput)
    {
        string result = input.ClearSymbols(useSpace);
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData(null, false)]
    public void ClearSymbols_ThrowException(string input, bool useSpace)
    {
        Action act = () => input.ClearSymbols(useSpace);
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData(@"Mëtàl!@#$%^&*()_+}{[]|:';<>,.?/~Hëàd", true, "Metal                           Head")]
    [InlineData(@"Mëtàl!@#$%^&*()_+}{[]|:';<>,.?/~Hëàd", false, "MetalHead")]
    [InlineData("MëtàlHëàd", true, "MetalHead")]
    [InlineData("MëtàlHëàd", false, "MetalHead")]
    [InlineData("", true, "")]
    [InlineData("", false, "")]
    public void ClearSpecialCharacters_ReturnsExpectedResult(string input, bool useSpace, string expectedOutput)
    {
        string result = input.ClearSpecialCharacters(useSpace);
        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData(null, false)]
    public void ClearSpecialCharacters_ThrowException(string input, bool useSpace)
    {
        Action act = () => input.ClearSpecialCharacters(useSpace);
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData("hello world", TextEncode.ASCII)]
    [InlineData("hello world", TextEncode.UTF8)]
    [InlineData("hello world", TextEncode.UTF16)]
    [InlineData("hello world", TextEncode.UTF32)]
    [InlineData("hello world", TextEncode.Unicode)]
    [InlineData("hello world", TextEncode.Latin1)]
    [InlineData("hello world", null)]
    public void StringToByteArray_ConvertsStringToByteArray(string inputString, TextEncode? textEncode)
    {
        byte[] expectedOutput;
        byte[] actualOutput;

        switch (textEncode)
        {
            case TextEncode.ASCII:
                {
                    expectedOutput = Encoding.ASCII.GetBytes(inputString);
                    break;
                }
            case TextEncode.UTF8:
                {
                    expectedOutput = Encoding.UTF8.GetBytes(inputString);
                    break;
                }
            case TextEncode.UTF16:
                {
                    expectedOutput = Encoding.BigEndianUnicode.GetBytes(inputString);
                    break;
                }
            case TextEncode.UTF32:
                {
                    expectedOutput = Encoding.UTF32.GetBytes(inputString);
                    break;
                }
            case TextEncode.Unicode:
                {
                    expectedOutput = Encoding.Unicode.GetBytes(inputString);
                    break;
                }
            case TextEncode.Latin1:
                {
                    expectedOutput = Encoding.Latin1.GetBytes(inputString);
                    break;
                }
            default:
                {
                    expectedOutput = Encoding.UTF8.GetBytes(inputString);
                    break;
                }
        }

        if (textEncode == null)
            actualOutput = inputString.StringToByteArray();
        else
            actualOutput = inputString.StringToByteArray((TextEncode)textEncode);

        actualOutput.Should().BeEquivalentTo(expectedOutput);
    }

    [Theory]
    [InlineData(null)]
    public void StringToByteArray_ConvertsStringToByteArray_ThrowException(string input)
    {
        Action act = () => input.StringToByteArray();
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Theory]
    [InlineData("hello world", TextEncode.ASCII)]
    [InlineData("hello world", TextEncode.UTF8)]
    [InlineData("hello world", TextEncode.UTF16)]
    [InlineData("hello world", TextEncode.UTF32)]
    [InlineData("hello world", TextEncode.Unicode)]
    [InlineData("hello world", TextEncode.Latin1)]
    [InlineData("hello world", null)]
    public void ByteArrayToString_ConvertsByteArrayToString(string expectedOutput, TextEncode? textEncode)
    {
        byte[] actualOutput;
        string result;

        switch (textEncode)
        {
            case TextEncode.ASCII:
                {
                    actualOutput = Encoding.ASCII.GetBytes(expectedOutput);
                    break;
                }
            case TextEncode.UTF8:
                {
                    actualOutput = Encoding.UTF8.GetBytes(expectedOutput);
                    break;
                }
            case TextEncode.UTF16:
                {
                    actualOutput = Encoding.BigEndianUnicode.GetBytes(expectedOutput);
                    break;
                }
            case TextEncode.UTF32:
                {
                    actualOutput = Encoding.UTF32.GetBytes(expectedOutput);
                    break;
                }
            case TextEncode.Unicode:
                {
                    actualOutput = Encoding.Unicode.GetBytes(expectedOutput);
                    break;
                }
            case TextEncode.Latin1:
                {
                    actualOutput = Encoding.Latin1.GetBytes(expectedOutput);
                    break;
                }
            default:
                {
                    actualOutput = Encoding.UTF8.GetBytes(expectedOutput);
                    break;
                }
        }

        if (textEncode == null)
            result = actualOutput.ByteArrayToString();
        else
            result = actualOutput.ByteArrayToString((TextEncode)textEncode);

        result.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(null)]
    public void ByteArrayToString_ConvertsByteArrayToString_ThrowException(byte[] input)
    {
        Action act = () => input.ByteArrayToString();
        act.Should().ThrowExactly<ArgumentNullException>();
    }

}