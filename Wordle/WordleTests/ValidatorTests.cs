using NUnit.Framework;
using Rhino.Mocks;
using Wordle; 

namespace WordleTests
{
    class ValidatorTests
    {
        // TODO - how to deal with code duplication, is that not as relavant in writing tests?

        [Test]
        public static void Validate_GuessIsEmpty_False()
        {
            // Arrange
            string userGuess = "";

            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(false);

            var guessValidator = new WordleValidator(mockEngDictionary);

            // Act
            var isValidWordleWord = guessValidator.Validate(userGuess);

            // Assert
            Assert.IsFalse(isValidWordleWord);
        }

        [Test]
        [TestCase("all")]
        [TestCase("byte")]
        [TestCase("remark")]
        [TestCase("acknowledge")]
        public static void Validate_GuessIsNot5Letters_False(string userGuess)
        {
            // Arrange
            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(true);

            var guessValidator = new WordleValidator(mockEngDictionary);

            // Act 
            var isValidWordleWord = guessValidator.Validate(userGuess);

            // Assert
            Assert.IsFalse(isValidWordleWord);
        }

        [Test]
        [TestCase("y'all")]
        [TestCase("te9nt")]
        [TestCase("$tart")]
        [TestCase("me at")]
        public static void Validate_GuessContainsNonLetter_False(string userGuess)
        {
            // Arrange
            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(false); 

            var guessValidator = new WordleValidator(mockEngDictionary);

            // Act 
            var isValidWordleWord = guessValidator.Validate(userGuess);

            // Assert
            Assert.IsFalse(isValidWordleWord);
        }

        [Test]
        [TestCase("eated")]
        public static void Validate_GuessNotInDictionary_False(string userGuess)
        {
            // Arrange 
            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(false);

            var guessValidator = new WordleValidator(mockEngDictionary);

            // Act 
            var isValidWordleWord = guessValidator.Validate(userGuess);

            // Assert
            Assert.IsFalse(isValidWordleWord);
        }


    }
}
