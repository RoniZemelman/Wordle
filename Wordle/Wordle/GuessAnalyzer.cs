using System.Linq;
using Wordle;

public class GuessAnalyzer : IGuessAnalyzer
{
    private readonly string answer;
    private bool[] letterIsMatched;

    public GuessAnalyzer(string answer)
    {
        this.answer = answer;
    }

    private void CheckForPartialMatch(char currGuessLetter, out bool foundPartialMatch)
    {
        foundPartialMatch = false;

        for (int answerIndex = 0; answerIndex < 5; ++answerIndex)
        { 
            if (currGuessLetter == answer[answerIndex] 
                && letterIsMatched[answerIndex] == false)
            {
                foundPartialMatch = true;

                letterIsMatched[answerIndex] = true;
                
                break;
            }
        }        
    }

    public GuessResult Analyze(string userGuess)
    {
        GuessResult guessResult = new GuessResult();
        letterIsMatched = new bool[5];

        int i = 0;
        foreach (char guessLetter in userGuess)
        {
            bool _isExactMatch = false;
            bool _isPartialMatch = false;

            if (guessLetter == answer[i])
            {
                _isExactMatch = true;

                letterIsMatched[i] = true;    
            }
            else 
            {
                CheckForPartialMatch(guessLetter, out _isPartialMatch);
            }

            guessResult.SetItemAt(i++, guessLetter,
                                isExactMatch: _isExactMatch, isPartialMatch: _isPartialMatch);
        }

        return guessResult;
    }

}
