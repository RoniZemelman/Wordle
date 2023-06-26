using System;
using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;

namespace WordleTests
{
    class AnswerGeneratorTests
    {
        [Test]
        public static void GenerateAnswer_EmptyStringParam_Null()
        {
            // arrange
            string[] emptyStringArray = Array.Empty<string>();
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();

            var answerGenerator = new AnswerGenerator(mockValidator, emptyStringArray);

            // act
            var answerResult = answerGenerator.GenerateAnswer();

            // assert
            Assert.IsNull(answerResult);
        }

        [Test]
        public static void GenerateAnswer_ValidatorValidatesOnlyWordInArray_ReturnsTheWord()
        {
            // arrange 
            var validWord = "smart";
            string[] arrayWith1ValidWord = { validWord };

            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(validator => validator.Validate(validWord))
                .Return(new ValidatorResult(true, true, true));

            var answerGenerator = new AnswerGenerator(mockValidator, arrayWith1ValidWord);
            
            // act
            var wordResult = answerGenerator.GenerateAnswer();
            
            // assert
            Assert.AreEqual(validWord, wordResult);
        }

        [Test]
        public static void GenerateAnswer_ValidatorInvalidatesAllWords_ReturnsNull()
        {
            // arrange 
            string[] arrayWith1ValidWord = { "invalidWord1", "invalidWord2" };

            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(validator => validator.Validate(""))
                .IgnoreArguments()
                .Return(new ValidatorResult()); 

            var answerGenerator = new AnswerGenerator(mockValidator, arrayWith1ValidWord);

            // act
            var wordResult = answerGenerator.GenerateAnswer();

            // assert
            Assert.IsNull(wordResult);
        }

        [Test] public static void GenerateAnswer_ValidatorInvalidates1WordValidatesOther_OnlyReturnsValidWord()
        {
            // arrange 
            string[] arrayWith1ValidWord = { "valid", "invalidWord2" };

            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(validator => validator.Validate("valid"))
                .Return(new ValidatorResult(true, true, true));
            mockValidator.Stub(validator => validator.Validate("invalidWord2"))
                .Return(new ValidatorResult());

            var answerGenerator = new AnswerGenerator(mockValidator, arrayWith1ValidWord);

            // act
            var wordResult1 = answerGenerator.GenerateAnswer();
            var wordResult2 = answerGenerator.GenerateAnswer();

            // assert
            Assert.AreEqual("valid", wordResult1);
            Assert.AreEqual("valid", wordResult2);
        }

    }
}
