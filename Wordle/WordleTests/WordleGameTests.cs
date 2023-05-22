using NUnit.Framework;
using Rhino.Mocks;
using Wordle;

namespace WordleTests
{
    class WordleGameTests
    {
        [Test]
        public static void Constructor_DefaultInitialization_Has5TurnsRemaining()
        {
            // Arrange
            string answer = "dontCare";

            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary("")).IgnoreArguments().Return(true);

            // Act
            var wordleGame = new WordleGame(answer, new WordleValidator(mockEngDictionary));

            // Assert
            Assert.AreEqual(5, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void ValidateGuess_UserGuessIsNot5Letters_ReturnsCorrectObject()
        {
            // Arrange
            string answer = "dontCare";
            string userGuess = "all";

            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(true);

            var wordleGame = new WordleGame(answer, new WordleValidator(mockEngDictionary));

            //act
           var validateResult = wordleGame.ValidateGuess(userGuess);

            // assert
            Assert.IsFalse(validateResult.Is5Letters);
        }

    }
}
