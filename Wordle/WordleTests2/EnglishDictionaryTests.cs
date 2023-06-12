using System.IO;
using System.Security.Cryptography;
using Castle.Core.Resource;
using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class EnglishDictionaryTests
    {
        private const string TestingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt";
        private static byte[] _fileContents;

        [OneTimeSetUp]
        public static void ReadFileContentsIntoMainMemory()
        {
            _fileContents = File.ReadAllBytes(TestingDictionaryFilePath);
        }

        [Test]
        [TestCase("blahblah")]
        [TestCase("shtuyot")]
        public static void IsInDictionary_WordNotInDictionary_False(string word)
        {
            //// Arrange
            var engDictionary = new EnglishDictionary(new MemoryStream(_fileContents));

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
            var engDictionary = new EnglishDictionary(new MemoryStream(_fileContents));

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }
    }
}
