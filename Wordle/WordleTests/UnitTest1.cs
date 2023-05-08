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
        


        // one partial match positions?
        // Integration test of partial and exact matches 
        // Add Edge Case: answer "sweat" and guess "xxeex" - the first e is 1 exact, 0 partial
    }
    
    


}