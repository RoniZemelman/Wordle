using System.IO;
using System.Security.Cryptography;
using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {
        private const string TestingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt.txt";
        // TODO - set up fixture - read files once

        [Test]
        [TestCase("blahblah")]
        [TestCase("shtuyot")]
        public static void IsInDictionary_WordNotInDictionary_False(string word)
        {
            // Arrange
            byte[] fileContents = File.ReadAllBytes(TestingDictionaryFilePath);
            var engDictionary = new EnglishDictionary(new MemoryStream(fileContents));

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
            byte[] fileContents = File.ReadAllBytes(TestingDictionaryFilePath);
            var engDictionary = new EnglishDictionary(new MemoryStream(fileContents));

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }
    }
}
