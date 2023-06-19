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
        // TODO remove filepath, configure MemoryStream in each test
        private const string TestingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt";
        private static byte[] _fileContents;
        private const int ExpectedNumOfValidWordleAnswers = 1379;

        [OneTimeSetUp]
        public static void ReadFileContentsIntoMainMemory()
        {
            _fileContents = File.ReadAllBytes(TestingDictionaryFilePath);
        }

        // TODO move the above to separate utils file - duplicated in EngDictionaryTests...
        // TODO: use case for numOfAnswers()?
        [Test]
        public static void NumOfAnswers_AnswerGeneratedCreated_ExpectedNumOfAnswers()
        {
            // Arrange
            var memoryStream = new MemoryStream(_fileContents);
            var answerGenerator = new AnswerGenerator(new EnglishDictionary(memoryStream));

            // Act
            var numOfAnswers = answerGenerator.NumOfAnswers();

            // Assert
            Assert.AreEqual(ExpectedNumOfValidWordleAnswers, numOfAnswers);
        }

        [Test]  // TODO rewrite, mock validator as param, 1 true, all false..
        public static void GenerateAnswer_MethodCalledForNumOfAnswers_EachAnswerIsValid()
        {
            // Arrange
            var memoryStream = new MemoryStream(_fileContents);
            var engDictionary = new EnglishDictionary(memoryStream);
            var wordleValidator = new GuessValidator(engDictionary);
            var answerGenerator = new AnswerGenerator(engDictionary);

            // Act 
            var answerIsValid = true;
            for (int count = 0;
                 answerIsValid && (count < answerGenerator.NumOfAnswers()); 
                 ++count)
            {
                var answer = answerGenerator.GenerateAnswer();
                var validationResult = wordleValidator.Validate(answer);
                answerIsValid = validationResult.IsValidGuess();
            }

            // Assert
            Assert.IsTrue(answerIsValid);
        }

    }
}
