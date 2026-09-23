using SitWordleApi.Words;

namespace SitWordleApi.Tests.Words;

public class WordListTests
{
    private static WordList Small() => new(
        validWords: ["apple", "crane"],
        answers: ["apple", "stone"]);

    [Theory]
    [InlineData("apple", true)]
    [InlineData("APPLE", true)]
    [InlineData(" apple ", true)]
    [InlineData("aaaaa", false)]
    [InlineData("app", false)]
    [InlineData("appl3", false)]
    public void IsValid_AcceptsRealWordsInAnyCaseAndRejectsJunk(string word, bool expected)
    {
        var list = Small();

        var actual = list.IsValid(word);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IsValid_TreatsEveryAnswerAsValidGuess()
    {
        var list = Small();

        Assert.True(list.IsValid("stone"));
    }

    [Fact]
    public void AnswerCount_AndAnswerAt_FollowInputOrder()
    {
        var list = Small();

        Assert.Equal(2, list.AnswerCount);
        Assert.Equal("apple", list.AnswerAt(0));
        Assert.Equal("stone", list.AnswerAt(1));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void AnswerAt_ThrowsWhenIndexOutOfRange(int index)
    {
        var list = Small();

        Assert.Throws<ArgumentOutOfRangeException>(() => list.AnswerAt(index));
    }

    [Fact]
    public void FromFiles_LoadsRealDataAndEveryAnswerIsValid()
    {
        var dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");

        var list = WordList.FromFiles(dataDirectory);

        Assert.True(list.AnswerCount > 5000);
        for (var i = 0; i < list.AnswerCount; i += 97)
            Assert.True(list.IsValid(list.AnswerAt(i)), $"answer at {i} is not valid");
    }
}
