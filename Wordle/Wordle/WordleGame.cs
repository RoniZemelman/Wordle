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
        public const int NumLettersInWord = 5;  // Part of the WordleGame Logic?

        private GuessAnalyzer guessAnalyzer; //string answer;
        private readonly IWordValidator validator;
        private int numTurnsRemaining;
        private State status;
        

        public WordleGame(string answer, IWordValidator validator)
        {
            this.guessAnalyzer = new GuessAnalyzer(answer); // Accept as param?
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

            var currGuessResult = guessAnalyzer.Analyze(userGuess);
            if (currGuessResult.IsCorrect())
            {
                status = State.Won;
            }

            --numTurnsRemaining;

            return currGuessResult; 
        }
    }
}
