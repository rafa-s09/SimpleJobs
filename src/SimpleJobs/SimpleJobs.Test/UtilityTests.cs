using SimpleJobs.Utility;

namespace SimpleJobs.Test;

public class UtilityTests
{
    private readonly string TextSample = "Lorem Ipsum";
    private readonly string TextSample2 = "Cabaré-do-Sr-Zé";

    [Test]
    public void TestGetUntilOrEmpty()
    {
        string? textEmpty = TextSample.GetUntilOrEmpty('$');
        string? textGet = TextSample.GetUntilOrEmpty('I');

        if (string.IsNullOrEmpty(textEmpty) && textGet.Equals("Lorem "))
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void TestGetUntil()
    {
        string textGet = TextSample.GetUntil('I');

        Assert.That(textGet, Is.EqualTo("Lorem "));

    }

    [Test]
    public void TestGetAfterOrEmpty()
    {
        string? textEmpty = TextSample.GetAfterOrEmpty('$');
        string? textGet = TextSample.GetAfterOrEmpty('I');

        if (string.IsNullOrEmpty(textEmpty) && textGet.Equals("psum"))
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void TestGetAfter()
    {
        string? textGet = TextSample.GetAfter('I');

        Assert.That(textGet, Is.EqualTo("psum"));
    }

    [Test]
    public void TestClearAccentedCharacters()
    {
        string? textGet = TextSample2.ClearAccentedCharacters();

        Assert.That(textGet, Is.EqualTo("Cabare-do-Sr-Ze"));
    }

    [Test]
    public void TestClearSymbols()
    {
        string? textGet = TextSample2.ClearSymbols();
        string? textGetSpaced = TextSample2.ClearSymbols(true);

        Assert.That(textGet.Equals("CabarédoSrZé") && textGetSpaced.Equals("Cabaré do Sr Zé"), Is.True);
    }

    [Test]
    public void TestClearSpecialCharacters()
    {
        string? textGet = TextSample2.ClearSpecialCharacters();
        string? textGetSpaced = TextSample2.ClearSpecialCharacters(true);

        Assert.That(textGet.Equals("CabaredoSrZe") && textGetSpaced.Equals("Cabare do Sr Ze"), Is.True);
    }

    [Test]
    public void TestStringToByteArray()
    {
        try
        {
            var test = TextSample.StringToByteArray();            
            Assert.That(test.GetType(), Is.EqualTo(typeof(byte[])));
        }
        catch 
        {
            Assert.Fail();
        }
    }

    [Test]
    public void TestByteArrayToString()
    {
        var testBytes = TextSample.StringToByteArray();
        var test = testBytes.ByteArrayToString();
        Assert.That(test, Is.EqualTo(TextSample));            
    }

}