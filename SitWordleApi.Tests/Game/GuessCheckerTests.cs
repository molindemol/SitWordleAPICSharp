using SitWordleApi.Game;

namespace SitWordleApi.Tests.Game;

public class GuessCheckerTests
{
    private static LetterStatus[] Parse(string pattern) =>
        pattern.Select(c => c switch
        {
            'c' => LetterStatus.Correct,
            'p' => LetterStatus.Present,
            'a' => LetterStatus.Absent,
            _ => throw new ArgumentException($"unknown pattern char '{c}'"),
        }).ToArray();

    [Theory]
    [InlineData("paars", "paars", "ccccc")]
    [InlineData("appel", "paars", "ppaaa")]
    [InlineData("aabbb", "abbba", "cpccp")]
    [InlineData("eerie", "there", "papac")]
    [InlineData("APPLE", "apple", "ccccc")]
    public void Check_ColoursLikeRealWordle(string guess, string answer, string expectedPattern)
    {
        var expected = Parse(expectedPattern);

        var actual = GuessChecker.Check(guess, answer);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Check_ThrowsWhenLengthsDiffer()
    {
        Assert.Throws<ArgumentException>(() => GuessChecker.Check("app", "apple"));
    }

    [Fact]
    public void Check_ReturnsOneStatusPerLetter()
    {
        var actual = GuessChecker.Check("crane", "stone");

        Assert.Equal(5, actual.Length);
    }
}
