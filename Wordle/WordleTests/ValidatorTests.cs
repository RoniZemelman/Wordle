﻿using NUnit.Framework;
using Rhino.Mocks;
using Wordle;

namespace WordleTests
{
    class ValidatorTests
    {
        private static WordleValidator.ValidatorResult ArrangeAndValidate(string userGuess, bool isInDictionary)
        {
            // Arrange
            var mockEngDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEngDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(isInDictionary);

            var guessValidator = new WordleValidator(mockEngDictionary);

            // Act
            return (WordleValidator.ValidatorResult) guessValidator.Validate(userGuess);
        }

        [Test]
        public static void Validate_GuessIsEmpty_Not5Letters()
        {
            var validateResult = ArrangeAndValidate(userGuess: "", isInDictionary: false);

            Assert.IsFalse(validateResult.Is5Letters);
        }

        [Test]
        [TestCase("all")]
        [TestCase("byte")]
        [TestCase("remark")]
        [TestCase("acknowledge")]
        public static void Validate_GuessIsNot5Letters_Not5Letters(string _userGuess)
        {
            var validateResult = ArrangeAndValidate(userGuess: _userGuess, isInDictionary: true);

            Assert.IsFalse(validateResult.Is5Letters);
        }

        [Test]
        [TestCase("y'all")]
        [TestCase("I'll")]
        public static void Validate_GuessContainsNonLetter_NotAllChars(string _userGuess) // TODO: Might be legal guess
        {
            var validateResult = ArrangeAndValidate(userGuess: _userGuess, isInDictionary: true);

            Assert.IsFalse(validateResult.IsAllChars);
        }

        [Test]
        [TestCase("eated")]
        public static void Validate_GuessNotInDictionary_NotInDictionary(string _userGuess)
        {
            var validateResult = ArrangeAndValidate(userGuess: _userGuess, isInDictionary: false);

            Assert.IsFalse(validateResult.IsInDictionary);
        }


    }
}
