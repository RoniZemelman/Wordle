

using System;
using Wordle;
using static Wordle.GuessValidator;

namespace Wordle
{
    public class WordleGame
    {
        public enum State  // convention for naming enums?
        {
            IsRunning,
            Won
        }

        public const int MaxNumOfTurns = 5;
        public const int NumLettersInWord = 5;  // Does this belong here?  Part of the WordleGame Logic...

        private readonly IWordValidator validator;
        private int numTurnsRemaining;
        private State status;

        public WordleGame(string answer, IWordValidator validator)
        {
            this.validator = validator;
            this.numTurnsRemaining = MaxNumOfTurns;
            this.status = State.IsRunning;
        }

        public State Status()
        {
            return status;
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

            if (new GuessAnalyzer("bingo").Analyze(userGuess).GetNumExactMatches() == 5)
            {
                status = State.Won;
            }

            --numTurnsRemaining;

            return new GuessResult(); // Neccessary to return guessResult to client? 
        }
    }
}
