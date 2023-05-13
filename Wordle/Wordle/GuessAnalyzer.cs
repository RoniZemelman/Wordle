using System.Linq;
using Wordle;

public class GuessAnalyzer
{
    private string answer;
    private bool[] letterIsMatched;

    public GuessAnalyzer(string answer)
    {
        this.answer = answer;
        // Note: letterIsMatched[] is lazily constructed in Analyze method
    }

    private void CheckForUnMatchedGuessLetterInAnswer(char currGuessLetter, out bool result)
    {
        result = false;

        for (int answerIndex = 0; answerIndex < 5; ++answerIndex)
        { 
            if (currGuessLetter == answer[answerIndex] 
                && letterIsMatched[answerIndex] == false)
            {
                letterIsMatched[answerIndex] = true;
                
                result = true;
                
                break;
            }
        }        
    }

    public GuessResult Analyze(string userGuess)
    {
        GuessResult guessResult = new GuessResult();
        letterIsMatched = new bool[5];

        int guessIndex = 0;
        foreach (char guessLetter in userGuess)
        {
            bool _isExactMatch = false;
            bool _isPartialMatch = false;

            if (guessLetter == answer[guessIndex])
            {
                _isExactMatch = true;

                letterIsMatched[guessIndex] = true;    
            }
            else 
            {
                CheckForUnMatchedGuessLetterInAnswer(guessLetter, out _isPartialMatch);
            }

            guessResult.setItemAt(guessIndex++, guessLetter,
                                isExactMatch: _isExactMatch, isPartialMatch: _isPartialMatch);
        }

        return guessResult;
    }

}
