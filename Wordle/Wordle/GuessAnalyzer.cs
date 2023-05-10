using System.Collections.Generic;
using System.Linq;
using Wordle;

public class GuessAnalyzer
{
    private string answer;

    public GuessAnalyzer(string answer)
    {
        this.answer = answer; 
    }

    private bool IsTheLetterAvailableForMatching(char currGuessLetter, bool [] isAlreadyMatched)
    {
        for (int answerIndex = 0; answerIndex < 5; ++answerIndex)
        {
            if (answer[answerIndex] == currGuessLetter && isAlreadyMatched[answerIndex] == false)
            {
                isAlreadyMatched[answerIndex] = true;
                return true;
            }
        }
        
        return false;
    }

    public GuessResult Analyze(string userGuess)
    {
        GuessResult currGuess = new GuessResult();
        var isAlreadyMatched = new bool[5]; 

        for (int guessIndex = 0; guessIndex < 5; ++guessIndex)
        {
            char currGuessLetter = userGuess[guessIndex];

            bool IsExactMatch = (currGuessLetter == answer[guessIndex]);
            
            if (IsExactMatch)
            {
                isAlreadyMatched[guessIndex] = true;
                currGuess.setItemAt(guessIndex, currGuessLetter, IsExactMatch, false);

                continue;
            }

            bool isPartialMatch = IsTheLetterAvailableForMatching(currGuessLetter, isAlreadyMatched); // answer.Any(letter => letter == currGuessLetter && isAlreadyMatched[i] == false);

            currGuess.setItemAt(guessIndex, currGuessLetter, IsExactMatch, isPartialMatch);
        }

        return currGuess;
    }

}
