using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class AnswerGeneratorTests
    {

        private const string TestingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt";
        private static byte[] _fileContents;
        private const int ExpectedNumOfAnswers = 1379;

        [OneTimeSetUp]
        public static void ReadFileContentsIntoMainMemory()
        {
            _fileContents = File.ReadAllBytes(TestingDictionaryFilePath);
        }

        // TODO move the above to separate utils file - shared by EngDictionaryTests...

        [Test] // Remove 
        public static void Constructor_AnswerGeneratorCreated_NotNull()
        {
            // Arrange
            var memoryStream = new MemoryStream(_fileContents);

            // Act
            var answerGenerator = new AnswerGenerator(new EnglishDictionary(memoryStream));

            // Assert
            Assert.IsNotNull(answerGenerator);
        }

        [Test]
        public static void NumOfAnswers_AnswerGeneratedCreated_ExpectedNumOfAnswers()
        {
            // Arrange
            var memoryStream = new MemoryStream(_fileContents);
            var answerGenerator = new AnswerGenerator(new EnglishDictionary(memoryStream));

            // Act
            var numOfAnswers = answerGenerator.NumOfAnswers();

            // Assert
            Assert.AreEqual(ExpectedNumOfAnswers, numOfAnswers);
        }

        [Test]  // Sanity Check test - attempt to cover/validate total answers provided to user
        public static void GenerateAnswer_MethodCalledForExpectedNumOfAnswers_EachAnswerIsValidWordle()
        {
            // Arrange
            var memoryStream = new MemoryStream(_fileContents);
            var engDictionary = new EnglishDictionary(memoryStream);
            var wordleValidator = new GuessValidator(engDictionary);

            var answerGenerator = new AnswerGenerator(engDictionary);

            // Act + Assert _expectedNumOfAnswersTimes...
            for (int count = 0; count < ExpectedNumOfAnswers; ++count)
            {
                var answer = answerGenerator.GenerateAnswer();
                var validateAnswerResult = wordleValidator.Validate(answer);

                Assert.IsTrue(validateAnswerResult.IsValidGuess());
            }
                // Maybe just assert Is5Letters + IsAllChars since we know its in the dictionary
        }

    }
}
