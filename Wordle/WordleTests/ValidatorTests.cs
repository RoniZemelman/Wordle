using NUnit.Framework;
using Wordle; 

namespace WordleTests
{
    class ValidatorTests
    {
        // IValidator (need dictionary lookup + Mocking of previous tests

        // TODO - test empty string


        [Test]
        [TestCase("all")]
        [TestCase("byte")]
        [TestCase("remark")]
        [TestCase("acknowledge")]
        [TestCase("letter")]
        public static void Validate_GuessIsNot5Letters_False(string userGuess)
        {
            var guessValidator = new WordleValidator();

            Assert.IsFalse(guessValidator.Validate(userGuess));
        }

        [Test]
        [TestCase("eaten")]
        [TestCase("alive")]
        [TestCase("crave")]
        [TestCase("start")]
        [TestCase("relax")]
        public static void Validate_GuessIs5LetterWord_True(string userGuess)// REMOVE
        {
            var guessValidator = new WordleValidator();

            Assert.IsTrue(guessValidator.Validate(userGuess));
        }

        [Test]
        [TestCase("rent!")]
        [TestCase("tent9")]
        [TestCase("^left")]
        [TestCase("$tart")]
        public static void Validate_GuessHasNonAsciiChar_False(string userGuess)
        {
            var guessValidator = new WordleValidator();
            Assert.IsFalse(guessValidator.Validate(userGuess));
        }

        [Test]
        [TestCase("eated")]
        [TestCase("relix")]
        [TestCase("tomao")]
        [TestCase("abcde")]
        [TestCase("lolol")]
        public static void Validate_GuessNotInDictionary_False(string userGuess)
        {
            //var englishDictionary = MockRepository.Stub<IEnglishDictionary>();
            //englishDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(false);

            // var guessValidator = new WordleValidator(englishDictionary);

            var guessValidator = new WordleValidator();

            Assert.IsFalse(guessValidator.Validate(userGuess));
        }


    }
}
