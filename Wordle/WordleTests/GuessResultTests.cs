using NUnit.Framework;
using Rhino.Mocks;
using Wordle;

namespace WordleTests
{
    class GuessResultTests
    {
        [Test]
        public void GuessResult_GuessHasVariableHits_LocationGettersReturnCorrectResult()
        {
            // Arrange
            string userGuess = "bxxxi";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer, mockWordValidator);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.IsTrue(guessResult.At(0).IsExactMatch());
            Assert.IsTrue(guessResult.At(1).Missed());
            Assert.IsTrue(guessResult.At(2).Missed());
            Assert.IsTrue(guessResult.At(3).Missed());
            Assert.IsTrue(guessResult.At(4).IsPartialMatch());
        }

        [Test]  // Sanity check - probably could test this logic earlier  -- REMOVE UNNEEDED TESTS
        public void GuessResult_GuessHasVariableHits_ExactAndPartialMatchesAreMutuallyExclusive()
        {
            // Arrange
            string userGuess = "bgnio";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer, mockWordValidator);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert 
            Assert.IsTrue(guessResult.At(0).IsExactMatch() == true
                            && guessResult.At(0).IsPartialMatch() == false

                            && guessResult.At(1).IsExactMatch() == false
                            && guessResult.At(1).IsPartialMatch() == true

                            && guessResult.At(2).IsExactMatch() == true
                            && guessResult.At(2).IsPartialMatch() == false

                            && guessResult.At(3).IsExactMatch() == false
                            && guessResult.At(3).IsPartialMatch() == true

                            && guessResult.At(4).IsExactMatch() == true
                            && guessResult.At(4).IsPartialMatch() == false);
        }


    }
}
