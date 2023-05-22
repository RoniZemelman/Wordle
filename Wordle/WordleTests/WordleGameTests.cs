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
            string answer = "dontCare";
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
        public static void PlayTurn_TurnInvokedWithBothValidAndInvalidGuesses_ValidatorInvoked()
        {
            // Arrange
            // Act

            // assert
            Assert.IsTrue(false);
        }
    }
}
