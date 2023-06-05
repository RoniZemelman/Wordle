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

        public static IGuessAnalyzer CreateMockGuessAnalyzerReturnsCorrect()
        {
            var mockGuessAnalzer = MockRepository.GenerateStub<IGuessAnalyzer>();

            GuessResult winningGuessResult = new GuessResult();

            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                winningGuessResult.SetItemAt(i, new GuessItem('*', isExactMatch: true,
                                                                        isPartialMatch: false));
            }

            mockGuessAnalzer.Stub(g => g.Analyze("")).IgnoreArguments().Return(winningGuessResult);

            return mockGuessAnalzer;
        }

        public static IGuessAnalyzer CreateMockGuessAnalyzerReturnsIncorrect()
        {
            var mockGuessAnalzer = MockRepository.GenerateStub<IGuessAnalyzer>();

            GuessResult incorrectGuessResult = new GuessResult();
            const char dontCare = '*';

            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                incorrectGuessResult.SetItemAt(i, new GuessItem(dontCare,
                                                                isExactMatch: false,
                                                                isPartialMatch: false));
            }

            mockGuessAnalzer.Stub(g => g.Analyze("")).IgnoreArguments().Return(incorrectGuessResult);

            return mockGuessAnalzer;
        }

    }
}
