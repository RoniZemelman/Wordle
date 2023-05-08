using System.Linq;
using Wordle;

public class GuessAnalyzer
{
    private string answer;

    public GuessAnalyzer(string answer)
    {
        this.answer = answer;
    }

    public GuessResult Analyze(string userGuess)
    {
        GuessResult currGuess = new GuessResult();

        for (int i = 0; i < 5; ++i)
        {
            char currLetter = userGuess[i];
            bool IsExactMatch = userGuess[i] == answer[i];
            bool isPartialMatch = !IsExactMatch && answer.Contains(currLetter);  

            currGuess.setItemAt(i, currLetter, IsExactMatch, isPartialMatch);
        }
        return currGuess;
    }

}
