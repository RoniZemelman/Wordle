using NUnit.Framework;
using Rhino.Mocks;
using Wordle; 

namespace WordleTests
{
    class ValidatorTests
    {

        // TODO - test empty string

        [Test]
        [TestCase("all")]
        [TestCase("byte")]
        [TestCase("remark")]
        [TestCase("acknowledge")]
        [TestCase("letter")]
        public static void Validate_GuessIsNot5Letters_False(string userGuess)
        {
            var englishDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            var guessValidator = new WordleValidator(englishDictionary);

            Assert.IsFalse(guessValidator.Validate(userGuess));
        }

        [Test]
        [TestCase("eaten")]
        [TestCase("alive")]
        [TestCase("crave")]
        [TestCase("start")]
        [TestCase("relax")]
        public static void Validate_GuessIs5LetterWord_True(string userGuess)
        {
            var englishDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            englishDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(true);


            var guessValidator = new WordleValidator(englishDictionary);

            Assert.IsTrue(guessValidator.Validate(userGuess));
        }

        [Test]
        [TestCase("rent!")]
        [TestCase("tent9")]
        [TestCase("^left")]
        [TestCase("$tart")]
        public static void Validate_GuessHasNonAsciiChar_False(string userGuess)
        {
            var englishDictionary = MockRepository.GenerateStub<IEnglishDictionary>();

            var guessValidator = new WordleValidator(englishDictionary);
            
            Assert.IsFalse(guessValidator.Validate(userGuess));
        }

        [Test]
        [TestCase("relix")]
        [TestCase("tomao")]
        [TestCase("abcde")]
        [TestCase("lolol")]
        [TestCase("eated")]
        public static void Validate_GuessNotInDictionary_False(string userGuess)
        {
            var englishDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            englishDictionary.Stub(d => d.IsInDictionary(userGuess)).Return(false);

            var guessValidator = new WordleValidator(englishDictionary);

            Assert.IsFalse(guessValidator.Validate(userGuess));
        }


    }
}
