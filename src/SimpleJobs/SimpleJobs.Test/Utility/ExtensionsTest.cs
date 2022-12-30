using AutoFixture;
using FluentAssertions;
using SimpleJobs.Utility;

namespace SimpleJobs.Test.Utility
{
    public class ExtensionsTest : BaseTest
    {
        #region GetUntilOrEmpty

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("bbbbb")]
        public void GetUntilOrEmpty_TextIsNullOrEmptyOrCharacterNotFound_ReturnStringEmpty(string text)
        {
            string result = text.GetUntilOrEmpty('a');

            result.Should().Be(string.Empty);
        }

        [Test]
        public void GetUntilOrEmpty_ValidText_ReturnSubString()
        {
            string text = Fixture.Create<string>();
            
            for (int pos = 0; pos < text.Length; pos++)
            {
                char character = text[pos];
                string expectedText = text[..text.IndexOf(character, StringComparison.Ordinal)];
                
                string result = text.GetUntilOrEmpty(text[pos]);

                result.Should().Be(expectedText);
            }
        }

        #endregion
    }
}