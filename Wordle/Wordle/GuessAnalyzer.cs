using System.Linq;
using Wordle;

public class GuessAnalyzer
{
    private string answer;

    public GuessAnalyzer(string answer)
    {
        this.answer = answer;
    }

    private bool IsFound(char currLetter)
    {
        return answer.Contains(currLetter);
    }

    private bool FoundLetterIsExactMatch(char currLetter, string userGuess)
    {
        int indexOfFoundLetter = answer.IndexOf(currLetter);
        
        return answer[indexOfFoundLetter] == userGuess[indexOfFoundLetter];
    }

    public GuessResult Analyze(string userGuess)
    {
        GuessResult currGuess = new GuessResult();

        for (int i = 0; i < 5; ++i)
        {
            char currLetter = userGuess[i];
            bool IsExactMatch = userGuess[i] == answer[i];
            bool isPartialMatch = !IsExactMatch && IsFound(currLetter)
                                && !FoundLetterIsExactMatch(currLetter, userGuess);  // TODO check all found locations?

            currGuess.setItemAt(i, currLetter, IsExactMatch, isPartialMatch);
        }
        return currGuess;
    }

}
