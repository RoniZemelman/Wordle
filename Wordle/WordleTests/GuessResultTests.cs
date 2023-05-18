using NUnit.Framework;
using Rhino.Mocks;
using Wordle;

namespace WordleTests
{
    class GuessResultTests
    {
        [Test]
        public void GuessResult_GuessHasExactMatch_CorrectLocationIsExactMatch()
        {
            // Arrange
            string userGuess = "bxxxi";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.IsTrue(guessResult.At(0).IsExactMatch());
        }

        [Test]
        public void GuessResult_GuessHasAtLeast1Miss_CorrectLocationIsMissed()
        {
            // Arrange
            string userGuess = "bxngo";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.IsTrue(guessResult.At(1).Missed());
        }

        [Test]
        public void GuessResult_GuessHasAtLeast1PartialMatch_CorrectLocationIsPartialMatch()
        {
            // Arrange
            string userGuess = "xxxxi";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.IsTrue(guessResult.At(4).IsPartialMatch());
        }

        [Test]  
        public void GuessResult_GuessHasAnExactMatch_LocationOfExactMatchIsNotPartialMatch()
        {
            // Arrange
            string userGuess = "bxxxx";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert 
            Assert.IsTrue(guessResult.At(0).IsExactMatch() == true
                            && guessResult.At(0).IsPartialMatch() == false);
        }


        [Test] // Not sure this test is logically neccessary
        public void GuessResult_GuessHasAPartialMatch_LocationOfPartialMatchIsNotExactMatch()
        {
            // Arrange
            string userGuess = "xgxxx";
            string answer = "bingo";

            var mockWordValidator = MockRepository.GenerateStub<IWordValidator>();
            mockWordValidator.Stub(v => v.Validate(userGuess)).Return(true);

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert 
            Assert.IsTrue(guessResult.At(1).IsPartialMatch() == true
                            && guessResult.At(1).IsExactMatch() == false);
        }

    }
}
