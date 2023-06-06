using Wordle;
using static Wordle.GuessResult;

public class GuessAnalyzer : IGuessAnalyzer
{
    private readonly string _answer;
    private bool[] _letterIsMatched;

    public GuessAnalyzer(string answer)
    {
        _answer = answer;
    }

    private void CheckForPartialMatch(char currGuessLetter, out bool foundPartialMatch)
    {
        foundPartialMatch = false;

        for (int answerIndex = 0; 
            answerIndex < WordleGame.NumLettersInWord; 
            ++answerIndex)
        { 
            if (currGuessLetter == _answer[answerIndex] 
                && _letterIsMatched[answerIndex] == false)
            {
                foundPartialMatch = true;

                _letterIsMatched[answerIndex] = true;
                
                break;
            }
        }        
    }

    public GuessResult Analyze(string userGuess)
    {
        GuessResult guessResult = new GuessResult();
        _letterIsMatched = new bool[WordleGame.NumLettersInWord];

        int i = 0;
        foreach (char guessLetter in userGuess)
        {
            bool _isExactMatch = false;
            bool _isPartialMatch = false;

            if (guessLetter == _answer[i])
            {
                _isExactMatch = true;

                _letterIsMatched[i] = true;    
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
