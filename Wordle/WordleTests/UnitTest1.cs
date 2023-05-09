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
        public void AnalyzeGuess_UserGuessHasNoHits_ReturnsZeroExactMatches()
        {
            string userGuess = "guess";
            string answer = "xxxxx";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumExactMatches() == 0); 
        }
        [Test]
        public void AnalyzeGuess_UserGuessHasAllHits_ReturnsCorrectNumExactMatches()
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
        public void AnalyzeGuess_UserGuessHasOneExactMatch_ReturnsCorrectNumExactMatches(string userGuess)
        {
            string answer = "bingo";

            //var guessValidator = MockRepos 

            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

             Assert.IsTrue(guessResult.GetNumExactMatches() == 1);            
        }

        [Test]
        public void AnalyzeGuess_UserGuessHasNoPartialMatches_ReturnsZeroPartialMatches()
        {
            string userGuess = "bingo";
            string answer = "xxxxx";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        [TestCase("xxxxb")]
        [TestCase("xxxxi")]
        [TestCase("xxxxn")]
        [TestCase("xxxxg")]
        [TestCase("xxxox")]
        public void AnalyzeGuess_UserGuessHasOnePartialMatch_ReturnsOnePartialMatch(string userGuess)
        {
            string answer = "bingo";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }
        
        [Test]
        public void AnalyzeGuess_UserGuessHasAllHits_ReturnsZeroPartialMatches()
        {
            string userGuess = "bingo";
            string answer = "bingo";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        /*********************************************** SANITY CHECK TESTS ******************************************/

        [Test]
        [TestCase("bxxxo", 2)]
        [TestCase("bxnxx", 2)]
        [TestCase("xixxo", 2)]
        [TestCase("bxxgo", 3)]
        [TestCase("xinxo", 3)]
        [TestCase("bixgx", 3)]
        [TestCase("bxngo", 4)]
        [TestCase("bixgo", 4)]
        [TestCase("binxo", 4)]
        public void AnalyzeGuess_UserHasVariableExactMatches_ReturnsCorrectResult(string userGuess, int expectedResult)
        {
            string answer = "bingo";

            // var guessValidator = MockRepos

            var analyzer = new GuessAnalyzer(answer);
            var guessResult = analyzer.Analyze(userGuess);

            Assert.That(guessResult.GetNumExactMatches(), Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("xoxix",2)]
        [TestCase("gxxxb", 2)]
        [TestCase("xgxix",2)]
        [TestCase("xxgon", 3)]
        [TestCase("ibxox", 3)]
        [TestCase("nxgxi", 3)]
        [TestCase("obinx", 4)]
        [TestCase("xnboi", 4)]
        [TestCase("onxbi", 4)]
        [TestCase("ibgon", 5)]
        [TestCase("ingob", 5)]
        [TestCase("ibgon", 5)]
        public void AnalyzeGuess_UserHasVariablePartialMatches_ReturnsCorrectResult(string userGuess, int expectedResult)
        {
            string answer = "bingo";

            // var guessValidator = MockRepos

            var analyzer = new GuessAnalyzer(answer);
            var guessResult = analyzer.Analyze(userGuess);

            Assert.That(guessResult.GetNumPartialMatches(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void AnalyzeGuess_UserGuessHas1Exact1Partial_ReturnsCorrectResult()
        {
            string userGuess = "bxxxi";
            string answer = "bingo";

            // var guessValidator = MockRepos

            var analyzer = new GuessAnalyzer(answer);
            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(1, guessResult.GetNumExactMatches());
            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }

        /*********************************************** END SANITY CHECK TESTS ******************************************/

        // Test Partial and Exact Matches 
        [Test]
        public void AnalyzeGuess_UserGuessesSameLetterTwice_ReturnsOneExactMatchZeroPartialMatches()
        {
            string userGuess = "xxeex";  // for example, the word "sweet"
            string answer = "sweat";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(1, guessResult.GetNumExactMatches());
            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        // Check positions of matches?
        // Integration test of partial and exact matches 
    }




}