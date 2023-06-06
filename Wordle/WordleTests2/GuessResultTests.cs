using NUnit.Framework;
using Rhino.Mocks;
using Wordle;

namespace WordleTests
{
    class GuessResultTests
    {
        static readonly string answer = "bingo";

        private static GuessResult ArrangeAndAnalyze(string userGuess)
        {
            var analyzer = new GuessAnalyzer(answer);

            return analyzer.Analyze(userGuess);
        }

        [Test]
        public void IsExactMatch_GuessHasExactMatch_CorrectLocationIsExactMatch()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "bxxxi");

            Assert.IsTrue(guessResult.At(0).IsExactMatch());
        }

        [Test]
        public void Missed_GuessHasAtLeast1Miss_CorrectLocationIsMissed()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "bxngo");

            Assert.IsTrue(guessResult.At(1).CompleteMiss());
        }

        [Test]
        public void IsPartialMatch_GuessHasAtLeast1PartialMatch_CorrectLocationIsPartialMatch()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "xxxxi");

            Assert.IsTrue(guessResult.At(4).IsPartialMatch());
        }

        [Test] // Not sure this test is logically neccessary
        public void IsExactMatchIsPartialMatch_GuessHasAnExactMatch_LocationOfExactMatchIsNotPartialMatch()
        {
            
            var guessResult = ArrangeAndAnalyze(userGuess: "bxxxx");

            Assert.IsTrue(guessResult.At(0).IsExactMatch() == true
                            && guessResult.At(0).IsPartialMatch() == false);
        }


        [Test] // Not sure this test is logically neccessary
        public void IsPartialMatchIsExactMatch_GuessHasAPartialMatch_LocationOfPartialMatchIsNotExactMatch()
        {
            
            var guessResult = ArrangeAndAnalyze(userGuess: "xgxxi");

            Assert.IsTrue(guessResult.At(1).IsPartialMatch() == true
                            && guessResult.At(1).IsExactMatch() == false);
        }

        [Test]
        public void IsCorrect_GuessHasAllExactMatches_IsCorrectWhenAllLettersMatch()
        {
            string userGuess = string.Copy(answer);
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(WordleGame.NumLettersInWord, guessResult.GetNumExactMatches());
            Assert.IsTrue(guessResult.IsCorrectGuess());
        }
    }
}
