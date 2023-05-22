

using System;
using Wordle;
using static Wordle.WordleValidator;

namespace Wordle
{
    public class WordleGame
    {
        private readonly IWordValidator validator;
        public WordleGame(string answer, IWordValidator validator)
        {
            this.validator = validator;
        }
    
        public int TurnsRemaining()
        {
            return 5;
        }

        public ValidatorResult ValidateGuess(string userGuess)
        {
            return (ValidatorResult) validator.Validate(userGuess);
        }
    }
}
