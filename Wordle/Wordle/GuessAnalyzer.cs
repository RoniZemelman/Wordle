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

    private void CheckForUnMatchedLetterInAnswer(char currGuessLetter, out bool result)
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
            if (guessLetter == answer[guessIndex])
            {
                letterIsMatched[guessIndex] = true; // Note: actually setting answerIndex,
                                                    // but here answerIndex == guessIndex

                guessResult.setItemAt(guessIndex++, guessLetter,
                                        isExactMatch: true, isPartialMatch: false);
                continue;
            }

            bool _isPartialMatch;

            CheckForUnMatchedLetterInAnswer(guessLetter, out _isPartialMatch);

            guessResult.setItemAt(guessIndex++, guessLetter,
                                isExactMatch: false, isPartialMatch: _isPartialMatch);
        }

        return guessResult;
    }

}
