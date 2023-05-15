using NUnit.Framework;

namespace WordleTests
{
    class GuessResultTests
    {
        [Test]
        public void GuessResult_GuessHasVariableHits_LocationGettersReturnCorrectResult()
        {
            string userGuess = "bxxxi";
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);

            // TODO mock out guess validator

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.At(0).IsExactMatch());
            Assert.IsTrue(guessResult.At(1).Missed());
            Assert.IsTrue(guessResult.At(2).Missed());
            Assert.IsTrue(guessResult.At(3).Missed());
            Assert.IsTrue(guessResult.At(4).IsPartialMatch());
        }

        [Test]  // Sanity check - probably could test this logic earlier  -- REMOVE UNNEEDED TESTS
        public void GuessResult_GuessHasVariableHits_ExactAndPartialMatchesAreMutuallyExclusive()
        {
            string userGuess = "bgnio";
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);

            // TODO mock out guess validator

            var guessResult = analyzer.Analyze(userGuess);

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
