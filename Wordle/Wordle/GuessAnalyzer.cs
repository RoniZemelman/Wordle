using Wordle;
using static Wordle.GuessResult;

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

        for (int answerIndex = 0; 
            answerIndex < WordleGame.NumLettersInWord; 
            ++answerIndex)
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
        letterIsMatched = new bool[WordleGame.NumLettersInWord];

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

            guessResult.SetItemAt(i++, new GuessItem(guessLetter, _isExactMatch, _isPartialMatch));
        }

        return guessResult;
    }

}
