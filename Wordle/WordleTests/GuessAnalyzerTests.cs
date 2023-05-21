using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    public class GuessAnalyzerTests
    {
        private static GuessResult ArrangeAndAnalyze(string userGuess, string answer)
        {
            var analyzer = new GuessAnalyzer(answer);

            return analyzer.Analyze(userGuess);
        }

        [Test]
        public static void Analyze_GuessHasNoHits_ZeroExactMatches()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "guess", answer: "xxxxx");
            
            Assert.IsTrue(guessResult.GetNumExactMatches() == 0); 
        }
        [Test]
        public void Analyze_GuessHasAllHits_FiveExactMatches()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "bingo", answer: "bingo");

            Assert.IsTrue(guessResult.GetNumExactMatches() == 5);
        }

        [Test]
        [TestCase("bxxxx")]
        [TestCase("xixxx")]
        [TestCase("xxnxx")]
        [TestCase("xxxgx")]
        [TestCase("xxxxo")]
        public void Analyze_GuessHasOneExactMatch_OneExactMatch(string _userGuess)
        {
            var guessResult = ArrangeAndAnalyze(userGuess: _userGuess, answer: "bingo");

            Assert.AreEqual(1, guessResult.GetNumExactMatches());            
        }

        [Test]
        public void Analyze_GuessHasAllMisses_ZeroPartialMatches()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "guess", answer: "xxxxx");

            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        [TestCase("xxxxb")]
        [TestCase("ixxxx")]
        [TestCase("xnxxx")]
        [TestCase("xxgxx")]
        [TestCase("xxxox")]
        public void Analyze_GuessHasOnePartialMatch_1PartialMatch(string _userGuess)
        {
            var guessResult = ArrangeAndAnalyze(userGuess: _userGuess, answer: "bingo");

            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }
        
        [Test]
        public void Analyze_GuessHasAllHits_ZeroPartialMatches()
        {
            
            var guessResult = ArrangeAndAnalyze(userGuess: "bingo", answer: "bingo");
        
            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        public void Analyze_GuessHasAllPartialMatches_ZeroExactMatches()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "ibgon", answer: "bingo");

            Assert.AreEqual(0, guessResult.GetNumExactMatches());
        }

        [Test]
        public void Analyze_GuessHas2RepeatedLettersOneIsExact_NoPartialMatch()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "sweet", answer: "sweat");

            Assert.AreEqual(0, guessResult.GetNumPartialMatches());
        }

        [Test]
        public void Analyze_GuessHasLetterRepeated3TimesTwoInAnswer_1PartialMatch()
        {
            var guessResult = ArrangeAndAnalyze(userGuess: "daddy", answer: "diced");

            Assert.AreEqual(1, guessResult.GetNumPartialMatches());
        }                     
     }
}