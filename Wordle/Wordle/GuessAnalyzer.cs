
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
            if (userGuess == answer)
            {
                currGuess.setItem(i, 'a', true, true);
            }
            else
            {
                currGuess.setItem(i, 'a', false, false);
            }
            
        }
        return currGuess;
    }

}
