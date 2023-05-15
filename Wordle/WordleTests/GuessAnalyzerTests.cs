using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    public class GuessAnalyzerTests
    {
        private const int lenOfWord = 5;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Analyze_GuessHasNoHits_ZeroExactMatches()
        {
            string userGuess = "guess";
            string answer = "xxxxx";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.IsTrue(guessResult.GetNumExactMatches() == 0); 
        }
        [Test]
        public void Analyze_GuessHasAllHits_FiveExactMatches()
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
        public void Analyze_GuessHasOneExactMatch_OneExactMatch(string userGuess)
        {
            string answer = "bingo";

            //var guessValidator = MockRepos 

            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

             Assert.IsTrue(guessResult.GetNumExactMatches() == 1);            
        }

        [Test]
        public void Analyze_GuessHasNoPartialMatch_ZeroPartialMatches()
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
        public void Analyze_GuessHasOnePartialMatch_OnePartialMatch(string userGuess)
        {
            string answer = "bingo";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }
        
        [Test]
        public void Analyze_GuessHasAllHits_ZeroPartialMatches()
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
        public void Analyze_GuessHasVariableNumExactMatches_ReturnsCorrectResult(string userGuess, int expectedResult)
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
        public void Analyze_GuessHasVariableNumPartialMatches_ReturnsCorrectResult(string userGuess, int expectedResult)
        {
            string answer = "bingo";

            // var guessValidator = MockRepos

            var analyzer = new GuessAnalyzer(answer);
            var guessResult = analyzer.Analyze(userGuess);

            Assert.That(guessResult.GetNumPartialMatches(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void Analyze_GuessHas1Exact1Partial_ReturnsCorrectResult()
        {
            string userGuess = "bxxxi";
            string answer = "bingo";

            // var guessValidator = MockRepos

            var analyzer = new GuessAnalyzer(answer);
            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(1, guessResult.GetNumExactMatches());
            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }

        /*****************************************************************************************/

        // Test Partial and Exact Matches 
        [Test]
        public void Analyze_GuessHas2RepeatedLetters1ExactMatch_1ExactMatchNoPartialMatch()
        {
            string userGuess = "sweet";  
            string answer = "sweat";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(4, guessResult.GetNumExactMatches());
            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        public void Analyze_GuessHasLetterRepeated3TimesTwoInAnswer_1ExactMatch1PartialMatch()
        {
            string userGuess = "daddy";
            string answer =    "diced";

            //var guessValidator = MockRepos
            var analyzer = new GuessAnalyzer(answer);

            var guessResult = analyzer.Analyze(userGuess);

            Assert.AreEqual(1, guessResult.GetNumExactMatches());
            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }
    }

}