﻿using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static Wordle.GuessResult;

namespace WordleTests
{
    class WordleGameTestsUtils
    {
        public static IWordValidator CreateAndConfigureMockValidator(bool validatorValue)
        {
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(validatorValue, validatorValue, validatorValue));

            return mockValidator;
        }

        private static IGuessAnalyzer CreateMockAnalyzer(bool _isExactMatch)
        {
            var mockGuessAnalyzer = MockRepository.GenerateStub<IGuessAnalyzer>();

            GuessResult guessResult = new GuessResult
            {
                ValidationResult = new ValidatorResult(true, true, true)
            };
            const char dontCare = '*';

            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                guessResult.SetItemAt(i, new GuessLetterResult(dontCare, isExactMatch: _isExactMatch,
                                                                        isPartialMatch: false));
            }

            mockGuessAnalyzer.Stub(g => g.Analyze("")).IgnoreArguments().Return(guessResult);

            return mockGuessAnalyzer;
        }

        public static IGuessAnalyzer CreateMockGuessAnalyzerThatAlwaysReturnsCorrect()
        {
            return CreateMockAnalyzer(true);
        }

        public static IGuessAnalyzer CreateMockGuessAnalyzerThatAlwaysReturnsIncorrect()
        {
            return CreateMockAnalyzer(false);
        }

    }
}
