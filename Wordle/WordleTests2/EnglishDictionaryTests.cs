using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {

        private static string testingDictionaryFilePath =
            "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt.txt";

        [Test]
        [TestCase("blahblah")]
        [TestCase("shtuyot")]
        public static void IsInDictionary_MadeUpWord_False(string word)
        {
            // Arrange
            var engDictionary = new EnglishDictionary(testingDictionaryFilePath);

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
            var engDictionary = new EnglishDictionary(testingDictionaryFilePath);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }

    }
}
