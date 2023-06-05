using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static Wordle.GuessResult;

namespace WordleTests
{
    class Utils
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
            var mockGuessAnalzer = MockRepository.GenerateStub<IGuessAnalyzer>();

            GuessResult guessResult = new GuessResult();
            const char dontCare = '*';

            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                guessResult.SetItemAt(i, new GuessItem(dontCare, isExactMatch: _isExactMatch,
                                                                        isPartialMatch: false));
            }

            mockGuessAnalzer.Stub(g => g.Analyze("")).IgnoreArguments().Return(guessResult);

            return mockGuessAnalzer;
        }

        public static IGuessAnalyzer CreateMockGuessAnalyzerReturnsCorrect()
        {
            return CreateMockAnalyzer(true);
        }

        public static IGuessAnalyzer CreateMockGuessAnalyzerReturnsIncorrect()
        {
            return CreateMockAnalyzer(false);
        }

    }
}
