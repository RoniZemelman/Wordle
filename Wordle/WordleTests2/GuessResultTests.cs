using NUnit.Framework;
using Rhino.Mocks;
using Wordle;

namespace WordleTests
{
    class GuessResultTests
    {
        private static readonly string Answer = "bingo";

        private static GuessResult ArrangeAndAnalyze(string userGuess)
        {
            var analyzer = new GuessAnalyzer(Answer);

            return analyzer.Analyze(userGuess);
        }

        [Test]
        public void IsExactMatch_GuessHasExactMatch_CorrectLocationIsExactMatch()
        {
            // Arrange
            var analyzer = new GuessAnalyzer(Answer);

            // Act
            var guessResult = analyzer.Analyze("bxxxi");

            // Assert
            Assert.IsTrue(guessResult.At(0).IsExactMatch());
        }

        [Test]
        public void Missed_GuessHasAtLeast1Miss_CorrectLocationIsMissed()
        {
            // Arrange
            var analyzer = new GuessAnalyzer(Answer);

            // Act
            var guessResult = analyzer.Analyze("bxngo");

            // Assert
            Assert.IsTrue(guessResult.At(1).CompleteMiss());
        }

        [Test]
        public void IsPartialMatch_GuessHasAtLeast1PartialMatch_CorrectLocationIsPartialMatch()
        {
            // Arrange
            var analyzer = new GuessAnalyzer(Answer);

            // Act
            var guessResult = ArrangeAndAnalyze(userGuess: "xxxxi");

            Assert.IsTrue(guessResult.At(4).IsPartialMatch());
        }

        [Test] // Not sure this test is logically neccessary
        public void IsExactMatchIsPartialMatch_GuessHasAnExactMatch_LocationOfExactMatchIsNotPartialMatch()
        {

            // Arrange
            var analyzer = new GuessAnalyzer(Answer);

            // Act
            var guessResult = ArrangeAndAnalyze(userGuess: "bxxxx");

            // Assert
            Assert.IsTrue(guessResult.At(0).IsExactMatch() == true
                            && guessResult.At(0).IsPartialMatch() == false);
        }


        [Test] // Not sure this test is logically neccessary
        public void IsPartialMatchIsExactMatch_GuessHasAPartialMatch_LocationOfPartialMatchIsNotExactMatch()
        {
            // Arrange
            var analyzer = new GuessAnalyzer(Answer);

            // Act
            var guessResult = ArrangeAndAnalyze(userGuess: "xgxxi");

            Assert.IsTrue(guessResult.At(1).IsPartialMatch() == true
                            && guessResult.At(1).IsExactMatch() == false);
        }

        [Test]
        public void IsCorrect_GuessHasAllExactMatches_IsCorrectWhenAllLettersMatch()
        {
            // Arrange
            string userGuess = Answer;
            var analyzer = new GuessAnalyzer(Answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);


            Assert.AreEqual(WordleGame.NumLettersInWord, guessResult.GetNumExactMatches());
            Assert.IsTrue(guessResult.IsCorrectGuess());
        }
    }
}
