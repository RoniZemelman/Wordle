using NUnit.Framework;
using Rhino.Mocks;
using Wordle; 

namespace WordleTests
{
    class ValidatorTests
    {

        private static bool ArrangeAndValidate(string userGuess, bool isInDictionary)
        {
            // Arrange
            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(isInDictionary);

            var guessValidator = new WordleValidator(mockEngDictionary);

            // Act
            return guessValidator.Validate(userGuess);
        }


        [Test]
        public static void Validate_GuessIsEmpty_False()
        {
            
            bool isValidWordleWord = ArrangeAndValidate(userGuess: "", isInDictionary: false);

            // Assert
            Assert.IsFalse(isValidWordleWord);
        }

        [Test]
        [TestCase("all")]
        [TestCase("byte")]
        [TestCase("remark")]
        [TestCase("acknowledge")]
        public static void Validate_GuessIsNot5Letters_False(string _userGuess)
        {
            var isValidWordleWord = ArrangeAndValidate(userGuess: _userGuess, isInDictionary: true);

            Assert.IsFalse(isValidWordleWord);
        }

        [Test]
        [TestCase("y'all")]
        [TestCase("I'll")]
        public static void Validate_GuessContainsNonLetter_False(string _userGuess) // TODO: Might be legal guess
        {
            var isValidWordleWord = ArrangeAndValidate(userGuess: _userGuess, isInDictionary: true);

            Assert.IsFalse(isValidWordleWord);
        }

        [Test]
        [TestCase("eated")]
        public static void Validate_GuessNotInDictionary_False(string _userGuess)
        {
            var isValidWordleWord = ArrangeAndValidate(userGuess: _userGuess, isInDictionary: false);

            // Assert
            Assert.IsFalse(isValidWordleWord);
        }


    }
}
