

using System;
using static Wordle.WordleValidator;

namespace Wordle
{
    public class WordleGame
    {
        public WordleGame(string answer, IWordValidator validator)
        {

        }
    
        public int TurnsRemaining()
        {
            return 5;
        }

        public ValidatorResult ValidateGuess(string userGuess)
        {
            return new ValidatorResult();
        }
    }
}
