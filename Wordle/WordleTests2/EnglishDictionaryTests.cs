using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {
        private const string TestingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt.txt";
        // TODO - create memory stream to pass into the EnglishDictionary
        // Readup on memory streams that read from main memory
        // TODO - set up - read files once

        [Test]
        [TestCase("blahblah")]
        [TestCase("shtuyot")]
        public static void IsInDictionary_WordNotInDictionary_False(string word)
        {
            // Arrange
            var engDictionary = new EnglishDictionary(TestingDictionaryFilePath);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);
            
            // Assert
            Assert.IsFalse(isInDictionary);
        }

        [Test]
        [TestCase("happy")]
        [TestCase("educational")]
        [TestCase("eat")]
        [TestCase("a")]
        public static void IsInDictionary_RealWordFromDictionaryFile_True(string word)
        {
            // Arrange
            var engDictionary = new EnglishDictionary(TestingDictionaryFilePath);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }
    }
}
