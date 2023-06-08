using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {
        [Test] 
        public static void Constructor_DictionaryInitialized_NotNull()
        {
            // Arrange
            var filePathDontCare = "";

            // Act
            var engDictionary = new EnglishDictionary(filePathDontCare);

            // Assert
            Assert.IsNotNull(engDictionary);
        }

        [Test]
        public static void IsInDictionary_MadeUpWord_False()
        {
            // Arrange
            var filePathDontCare = "";
            var word = "blahblah";
            var engDictionary = new EnglishDictionary(filePathDontCare);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);
            
            // Assert
            Assert.IsFalse(isInDictionary);
        }

        [Test]
        public static void IsInDictionary_RealWordFromDictionaryFile_True()
        {
            // Arrange
            var filePathDontCare = "";
            var word = "happy";
            var engDictionary = new EnglishDictionary(filePathDontCare);

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }

    }
}
