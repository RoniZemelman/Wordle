using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.WordleValidator;

namespace WordleTests
{
    class WordleGameTests
    { 
        [Test]
        public static void Constructor_WordleGameCreated_MaxNumTurnsRemaining()
        {
            // Arrange
            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary("")).IgnoreArguments().Return(true);

            // Act
            var wordleGame = new WordleGame(new WordleValidator(mockEngDictionary));

            // Assert
            Assert.AreEqual(WordleGame.MaxNumOfTurns, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_RemainingTurnsUnchanged()
        {
            // Arrange
            // TODO - pass in answer to WordleGame?
            string userGuess = "blablah";

            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess))
                .IgnoreArguments()
                .Return(new ValidatorResult(false, false, false));

            var wordleGame = new WordleGame(mockValidator);
            int turnsRemaining = wordleGame.TurnsRemaining();

            //act
            wordleGame.PlayTurn(userGuess);

            // assert
            Assert.AreEqual(turnsRemaining, wordleGame.TurnsRemaining());
        }

        [Test]
        [TestCase("valid")]
        [TestCase("NotValid")]
        public static void PlayTurn_PlayTurnInvokedWithUserGuess_ValidatorIsCalled(string userGuess)
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(null); 

            var wordleGame = new WordleGame(mockValidator);

            // Act
            wordleGame.PlayTurn(userGuess);

            // assert
            mockValidator.AssertWasCalled(v => v.Validate(userGuess));
        }

        public static void PlayTurn_ValidatorValidatesUserGuess_GuessAnalyzerIsCalled()
        {
            Assert.IsTrue(false);
        }
    }
}
