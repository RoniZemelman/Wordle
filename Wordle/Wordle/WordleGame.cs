

using System;
using Wordle;
using static Wordle.GuessValidator;

namespace Wordle
{
    public class WordleGame
    {
        public enum state
        {
            IsRunning
        }

        public const int MaxNumOfTurns = 5;

        private readonly IWordValidator validator;
        private int numTurnsRemaining;

        public WordleGame(IWordValidator validator)
        {
            this.validator = validator;
            this.numTurnsRemaining = MaxNumOfTurns; 
        }

        public state Status()
        {
            return state.IsRunning;
        }

        public int TurnsRemaining()
        {
            return numTurnsRemaining;
        }
        public GuessResult PlayTurn(string userGuess)
        {
            var validatorResult = validator.Validate(userGuess);

            if (validatorResult.IsValidGuess())
            {
                --numTurnsRemaining;
            }

            return new GuessResult();
        }
    }
}
