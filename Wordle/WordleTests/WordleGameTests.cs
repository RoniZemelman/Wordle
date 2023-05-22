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
        public static void ValidateGuess_UserGuessIsNot5Letters_False()
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
            Assert.IsFalse(validateResult.IsValidGuess());
        }

        [Test]
        public static void ValidateGuess_UserGuessIsNotAllChars_False()
        {
            // Arrange
            string answer = "dontCare";
            string userGuess = "y'all";

            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(true);

            var wordleGame = new WordleGame(answer, new WordleValidator(mockEngDictionary));

            //act
            var validateResult = wordleGame.ValidateGuess(userGuess);

            // assert
            Assert.IsFalse(validateResult.IsValidGuess());
        }

        [Test]
        public static void ValidateGuess_UserGuessIsNotInDictionary_False()
        {
            // Arrange
            string answer = "dontCare";
            string userGuess = "xxxxx";

            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(false);

            var wordleGame = new WordleGame(answer, new WordleValidator(mockEngDictionary));

            //act
            var validateResult = wordleGame.ValidateGuess(userGuess);

            // assert
            Assert.IsFalse(validateResult.IsValidGuess());
        }

        [Test]
        [TestCase("start")]
        [TestCase("eaten")]
        public static void ValidateGuess_UserGuessIsValid_True(string userGuess)
        {
            // Arrange
            string answer = "dontCare";
           

            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(true);

            var wordleGame = new WordleGame(answer, new WordleValidator(mockEngDictionary));

            //act
            var validateResult = wordleGame.ValidateGuess(userGuess);

            // assert
            Assert.IsTrue(validateResult.IsValidGuess());
        }

    }
}
