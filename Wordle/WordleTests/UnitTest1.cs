using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //test condition of user guess, e.g. length (5 chars), dictionary word etc
        //maybe we need a UserGuessValidator? Maybe the analyzer is initialized with one?
        [Test]
        public void AnalyzeGuess_UserGuessHasNoHits_ReturnsCorrectResult()
        {
            string userGuess = "guess";
            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer();

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetPartialMatches() == 0 
                    && guessResult.GetExactMatches() == 0);
        }

        //test case of all hits, partial matches, exact and partial etc.
    }
}