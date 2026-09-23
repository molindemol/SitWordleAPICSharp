namespace SitWordleApi.Game;
public static class GuessChecker
{
    public static LetterStatus[] Check(string guess, string answer)
    {
        var g = guess.ToLowerInvariant();
        var a = answer.ToLowerInvariant();
        if (g.Length != a.Length)
        {
            throw new ArgumentException($"guess and answer must have the same length ({g.Length} vs {a.Length})");
        }
            

        LetterStatus[] result = new LetterStatus[g.Length];
        Array.Fill(result, LetterStatus.Absent);

        char?[] remaining = [.. a.Select(c => (char?)c)];

        for (int i = 0; i < g.Length; i++)
        {
            if (g[i] == a[i])
            {
                result[i] = LetterStatus.Correct;
                remaining[i] = null;
            }
        }

        for (int i = 0; i < g.Length; i++ )
        {
            if (result[i] == LetterStatus.Correct) continue;
            var found = Array.IndexOf(remaining, (char?)g[i]);
            if (found != -1)
            {
                result[i] = LetterStatus.Present;
                remaining[found] = null; 
            }
                
        }

        return result;
    }
}