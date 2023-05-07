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

            Assert.IsTrue(guessResult.GetNumExactMatches() == 0); // Only testing exact matches right now
        }
        [Test]
        public void AnalyzeGuess_UserGuessHasAllHits_ReturnsCorrectResult()
        {
            string userGuess = "bingo";
            string answer = "bingo";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer); 

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumExactMatches() == 5);
        }
        [Test]
        [TestCase("bxxxx")]
        [TestCase("xixxx")]
        [TestCase("xxnxx")]
        [TestCase("xxxgx")]
        [TestCase("xxxxo")]
        public void AnalyzeGuess_UserGuessHasOneExactMatch_ReturnsCorrectResult(string userGuess)
        {
            string answer = "bingo";

            //var guessValidator = MockRepos // GuessValidator should be mocked out
                                            // to isolate testing of functionality of Analyzer
            
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumExactMatches() == 1);            
        }

        [Test]
        public void AnalyzeGuess_UserHasOnePartialHit_ReturnsCorrectResult()
        {
            string userGuess = "xxxxa";
            string answer = "abcde";

            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            System.Console.WriteLine(" guess Partial hits " + guessResult.GetNumPartialMatches());
            System.Console.WriteLine(" guess Exact hits " + guessResult.GetNumExactMatches());

            Assert.IsTrue(guessResult.GetNumPartialMatches() == 1);
        }

        // Next: Test one (yellow) partial hit in all guess spots. 

        // partial matches, exact and partial etc.
    }
}