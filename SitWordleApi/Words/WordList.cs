namespace SitWordleApi.Words;
using System.Text.RegularExpressions;

public sealed partial class WordList
{
    private readonly HashSet<string> valid;
    private readonly string[] answers;
    public WordList(IEnumerable<string> validWords, IEnumerable<string> answers)
    {
        this.answers = answers.Select(Normalize).Where(w => w.Length > 0).ToArray();
        valid = [.. validWords.Select(Normalize).Where(w => w.Length > 0)];
        valid.UnionWith(this.answers);
    }

    public static WordList FromFiles(string dataDirectory)
    {
        var validWords = File.ReadLines(Path.Combine(dataDirectory, "validWords.txt"));
        var answers = File.ReadLines(Path.Combine(dataDirectory, "answers.txt"));
        return new WordList(validWords, answers);
    }

    public static string Normalize(string word)
    {
        return word.Trim().ToLowerInvariant();
    }
    public bool IsValid(string word)
    {
        var normalized = Normalize(word);
        return WordPattern().IsMatch(normalized) && valid.Contains(normalized);
    }
    public int AnswerCount => answers.Length;
    public string AnswerAt(int index)
    {
        if (index < 0 || index >= answers.Length)
            throw new ArgumentOutOfRangeException(nameof(index), index, "answer index out of range");
        return answers[index];
    }

    [GeneratedRegex("^[a-z]{5}$")]
    private static partial Regex WordPattern();
}