using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    public class Tests
    {
        private const int lenOfWord = 5;

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
            string answer = "xxxxx";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumPartialMatches() == 0 
                    && guessResult.GetNumExactMatches() == 0);
        }
        [Test]
        public void AnalyzeGuess_UserGuessHasAllHits_ReturnsCorrectResult()
        {
            string userGuess = "bingo";
            string answer = "bingo";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer); 

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumPartialMatches() == 5
                    && guessResult.GetNumExactMatches() == 5);
        }
        [Test]
        [TestCase("bxxxx")]
        [TestCase("xixxx")]
        [TestCase("xxnxx")]
        [TestCase("xxxgx")]
        [TestCase("xxxxo")]
        public void AnalyzeGuess_UserGuessHasOneExactMatch_ReturnsCorrectResult(string answer)
        {
            string userGuess = "bingo";

            //var guessValidator = MockRepos // GuessValidator should be mocked out
                                            // to isolate testing of functionality
            
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumExactMatches() == 1);            
        }

        // test case of all hits, partial matches, exact and partial etc.
    }
}