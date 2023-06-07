
namespace Wordle
{
    public class WordleGame
    {
        public const int MaxNumOfTurns = 5;
        public const int NumLettersInWord = 5;  

        public enum State  // convention for naming enums?
        {
            IsAlive,
            Won,
            Lost
        }

        private readonly IGuessAnalyzer _guessAnalyzer; 
        private readonly IWordValidator _validator;
        private int _numTurnsRemaining;
        private State _status;

        public WordleGame(IGuessAnalyzer guessAnalyzer, IWordValidator validator)
        {
            _guessAnalyzer = guessAnalyzer; 
            _validator = validator;
            _numTurnsRemaining = MaxNumOfTurns;
            _status = State.IsAlive;
        }

        public State Status()
        {
            return _status;
        }

        public int TurnsRemaining()
        {
            return _numTurnsRemaining;
        }

        private void UpdateGameState(GuessResult guessResult)
        {
            --_numTurnsRemaining;

            if (_numTurnsRemaining == 0) 
            {
                _status = State.Lost;
            }

            if (guessResult.IsCorrectGuess())
            {
                _status = State.Won;
            }            
        }

        public GuessResult PlayTurn(string userGuess)
        {
            var guessResult = new GuessResult
            {
                ValidationResult = _validator.Validate(userGuess)
            };

            if (!guessResult.IsValid())
            {
                return guessResult;
            }

            guessResult = _guessAnalyzer.Analyze(userGuess); 
            
            UpdateGameState(guessResult);

            return guessResult; 
        }
    }
}
