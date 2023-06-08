using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {
        private static string dictionaryFilePath =
            "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt.txt";

        [Test] 
        public static void Constructor_DictionaryInitialized_NotNull()
        {
            // Arrange

            // Act
            var engDictionary = new EnglishDictionary(dictionaryFilePath);

            // Assert
            Assert.IsNotNull(engDictionary);
        }

        [Test]
        public static void IsInDictionary_MadeUpWord_False()
        {
            // Arrange
            var word = "blahblah";
            var engDictionary = new EnglishDictionary(dictionaryFilePath);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);
            
            // Assert
            Assert.IsFalse(isInDictionary);
        }

        [Test]
        public static void IsInDictionary_RealWordFromDictionaryFile_True()
        {
            // Arrange
            var word = "happy";
            var engDictionary = new EnglishDictionary(dictionaryFilePath);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }

    }
}
