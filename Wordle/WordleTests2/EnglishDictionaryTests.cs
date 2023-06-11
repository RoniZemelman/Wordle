using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {
        private const string _testingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt.txt";

        // TODO global setup of 1 EnglishDictionary (limit IO ops)


        [Test]
        [TestCase("blahblah")]
        [TestCase("shtuyot")]
        public static void IsInDictionary_MadeUpWord_False(string word)
        {
            // Arrange
            var engDictionary = new EnglishDictionary(_testingDictionaryFilePath);

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
            var engDictionary = new EnglishDictionary(_testingDictionaryFilePath);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }
    }
}
