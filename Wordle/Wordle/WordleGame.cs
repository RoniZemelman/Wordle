

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
        public WordleGame(IWordValidator validator)
        {
            this.validator = validator;
        }

        public state Status()
        {
            return state.IsRunning;
        }

        public int TurnsRemaining()
        {
            return 5;
        }
        public GuessResult PlayTurn(string userGuess)
        {
            var validatorResult = validator.Validate(userGuess);

            return new GuessResult();
        }
    }
}
