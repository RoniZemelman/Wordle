using NUnit.Framework;
using Rhino.Mocks;
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
            // Arrange
            string userGuess = "guess";
            string answer = "xxxxx";

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert 
            Assert.IsTrue(guessResult.GetNumExactMatches() == 0); 
        }
        [Test]
        public void Analyze_GuessHasAllHits_FiveExactMatches()
        {
            // Arrange 
            string userGuess = "bingo";
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
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
            // Arrange
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.IsTrue(guessResult.GetNumExactMatches() == 1);            
        }

        [Test]
        public void Analyze_GuessHasAllMisses_ZeroPartialMatches()
        {
            // Arrange 
            string userGuess = "bingo";
            string answer = "xxxxx";

            var analyzer = new GuessAnalyzer(answer);
            
            // Act
            var guessResult = analyzer.Analyze(userGuess);
            
            // Assert
            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        [TestCase("xxxxb")]
        [TestCase("ixxxx")]
        [TestCase("xnxxx")]
        [TestCase("xxgxx")]
        [TestCase("xxxox")]
        public void Analyze_GuessHasOnePartialMatch_1PartialMatch(string userGuess)
        {
            // Arrange
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);
            
            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }
        
        [Test]
        public void Analyze_GuessHasAllHits_ZeroPartialMatches()
        {
            // Arrange 
            string userGuess = "bingo";
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);
            
            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        public void Analyze_GuessHasAllPartialMatches_ZeroExactMatches()
        {
            // Arrange 
            string userGuess = "ibgon";
            string answer = "bingo";

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.AreEqual(0, guessResult.GetNumExactMatches());
        }

        [Test]
        public void Analyze_GuessHas2RepeatedLettersOneIsExact_NoPartialMatch()
        {
            // Arrange
            string userGuess = "sweet";  
            string answer = "sweat";

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        public void Analyze_GuessHasLetterRepeated3TimesTwoInAnswer_1PartialMatch()
        {
            // Arrange
            string userGuess = "daddy";
            string answer =    "diced";

            var analyzer = new GuessAnalyzer(answer);

            // Act
            var guessResult = analyzer.Analyze(userGuess);

            // Assert
            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }                     
     }
}