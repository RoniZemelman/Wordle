
using System.Linq;
using static Wordle.GuessValidator;

namespace Wordle
{
    public class GuessResult  
    {
        public class GuessLetterResult
        {
            private readonly char _letter; // TODO remove? 
            private readonly bool _isExactMatch;
            private readonly bool _isPartialMatch;

            public GuessLetterResult(char letter, bool isExactMatch = false, bool isPartialMatch = false)
            {
                _letter = letter;
                _isExactMatch = isExactMatch;
                _isPartialMatch = isPartialMatch;
            }

            public bool IsExactMatch() { return _isExactMatch; }

            public bool IsPartialMatch() { return _isPartialMatch; }

            public bool CompleteMiss() { return !_isExactMatch && !_isPartialMatch; }
        }

        public ValidatorResult ValidationResult;
        private readonly GuessLetterResult[] _guessAnalysisResults;
        public GuessResult()
        {
            ValidationResult = null;
            _guessAnalysisResults = new GuessLetterResult[WordleGame.NumLettersInWord];
        }
        
        public void SetItemAt(int index, GuessLetterResult guessLetterResult)
        {
            _guessAnalysisResults[index] = guessLetterResult;
        }

        public bool IsValid()
        {
            return ValidationResult.IsValidGuess();
        }

        public bool IsCorrectGuess()
        {
            return GetNumExactMatches() == WordleGame.NumLettersInWord;
        }

        public int GetNumExactMatches()
        {
            return _guessAnalysisResults.Count(g => g.IsExactMatch() == true);
        }

        public int GetNumPartialMatches()
        {
            return _guessAnalysisResults.Count(g => g.IsPartialMatch() == true);  
        }

        public GuessLetterResult At(int index)
        {
            return _guessAnalysisResults[index];
        }
    }

    
}
