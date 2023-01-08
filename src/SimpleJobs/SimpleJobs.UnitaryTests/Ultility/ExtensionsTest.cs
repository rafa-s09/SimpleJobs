namespace SimpleJobs.UnitaryTests.Utility;

public class ExtensionsTest : BaseTest
{
    private static string GenerateStringWithAllCharacters()
    {
        StringBuilder text = new();

        for (int i = 33; i <= 254; i++)
            text.Append((char)i);

        return text.ToString();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("bbbbb")]
    public void GetUntilOrEmpty_TextIsNullOrEmptyOrCharacterNotFound_ReturnStringEmpty(string text)
    {
        string result = text.GetUntilOrEmpty('5');

        result.Should().Be(string.Empty);
    }

    [Fact]
    public void GetUntilOrEmpty_ValidText_ReturnSubString()
    {
        string text = GenerateStringWithAllCharacters();

        for (int pos = 0; pos < text.Length; pos++)
        {
            char character = text[pos];
            string expectedText = text[..text.IndexOf(character, StringComparison.Ordinal)];

            string result = text.GetUntilOrEmpty(text[pos]);

            result.Should().Be(expectedText);
        }
    }
}