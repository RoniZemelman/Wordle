
using System;
using System.Linq;
using static Wordle.GuessValidator;

namespace Wordle
{
    public class GuessResult  
    {
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

        private int CountMatches(Func<GuessLetterResult, bool> matchCondition)
        {
            if (!IsValid())
                throw new InvalidOperationException("GuessResult is in Invalid state");

            return _guessAnalysisResults.Count(matchCondition);
        }

        public int GetNumExactMatches()
        {
            return CountMatches(g => g.IsExactMatch());
        }

        public int GetNumPartialMatches()
        {
            return CountMatches(g => g.IsPartialMatch());
        }

        public GuessLetterResult At(int index)
        {
            if (!IsValid())
                throw new InvalidOperationException("GuessResult is in Invalid state");

            return _guessAnalysisResults[index];
        }

        public bool IsValid()
        {
            return ValidationResult.IsValidGuess();
        }
        public bool IsNull()
        {
            var numOfNulls = _guessAnalysisResults.Count(g => g == null);

            return numOfNulls == WordleGame.NumLettersInWord;
        }
        public bool IsCorrectGuess()
        {
            return GetNumExactMatches() == WordleGame.NumLettersInWord;
        }

        public int Length()
        {
            return _guessAnalysisResults.Length;
        }
    }

    public class GuessLetterResult
    {
        public char Letter { get; }
        private readonly bool _isExactMatch;
        private readonly bool _isPartialMatch;

        public GuessLetterResult(char letter, bool isExactMatch = false, bool isPartialMatch = false)
        {
            Letter = letter;
            _isExactMatch = isExactMatch;
            _isPartialMatch = isPartialMatch;
        }

        public bool IsExactMatch() { return _isExactMatch; }

        public bool IsPartialMatch() { return _isPartialMatch; }

        public bool CompleteMiss() { return !_isExactMatch && !_isPartialMatch; }
    }
}
