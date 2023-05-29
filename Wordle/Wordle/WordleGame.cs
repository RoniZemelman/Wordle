using static Wordle.GuessValidator;

namespace Wordle
{
    public class WordleGame
    {
        public const int MaxNumOfTurns = 5;
        public const int NumLettersInWord = 5;  // Part of the WordleGame Logic?

        public enum State  // convention for naming enums?
        {
            IsRunning,
            Won,
            Lost
        }

        private IGuessAnalyzer guessAnalyzer; 
        private readonly IWordValidator validator;
        private int numTurnsRemaining;
        private State status;

        public WordleGame(IGuessAnalyzer guessAnalyzer, IWordValidator validator)
        {
            this.guessAnalyzer = guessAnalyzer; 
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

        private void UpdateGameState(GuessResult guessResult)
        {
            --numTurnsRemaining;

            if (numTurnsRemaining == 0) 
            {
                status = State.Lost;
            }

            if (guessResult.IsCorrectGuess())
            {
                status = State.Won;
            }            
        }

        public GuessResult PlayTurn(string userGuess, out ValidatorResult validatorOutResult)
        {
            validatorOutResult = validator.Validate(userGuess);

            if (!validatorOutResult.IsValidGuess())
            {
                return null;
            }

            var currGuessResult = guessAnalyzer.Analyze(userGuess);

            UpdateGameState(currGuessResult);
            
            return currGuessResult; 
        }
    }
}
