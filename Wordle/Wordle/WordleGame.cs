

using System;
using Wordle;
using static Wordle.GuessValidator;

namespace Wordle
{
    public class WordleGame
    {
        public enum State  // convention for naming enums?
        {
            IsRunning
        }

        public const int MaxNumOfTurns = 5;
        public const int NumLettersInWord = 5;  // Does this belong here?  Part of the WordleGame Logic...

        private readonly IWordValidator validator;
        private int numTurnsRemaining;

        public WordleGame(IWordValidator validator)
        {
            this.validator = validator;
            this.numTurnsRemaining = MaxNumOfTurns; 
        }

        public State Status()
        {
            return State.IsRunning;
        }

        public int TurnsRemaining()
        {
            return numTurnsRemaining;
        }
        public GuessResult PlayTurn(string userGuess, out ValidatorResult validatorResult)
        {
            validatorResult = validator.Validate(userGuess);

            if (!validatorResult.IsValidGuess())
            {
                return null;
            }
            
            --numTurnsRemaining;

            return new GuessResult();
        }
    }
}
