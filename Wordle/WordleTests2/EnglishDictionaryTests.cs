using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wordle;


namespace WordleTests
{
    class EnglishDictionaryTests
    {
        [Test]
        public static void IsInDictionary_WordNotInMemoryStream_False()
        {
            // Arrange
            string[] arrayOfWords = {"all\n", "eat\n"};
            var wordsAsBytes = arrayOfWords.SelectMany(word => Encoding.ASCII.GetBytes(word)).ToArray();

            var engDictionary = new EnglishDictionary(new MemoryStream(wordsAsBytes)); // _fileContents));

            // Act
            var isInDictionary = engDictionary.IsInDictionary("blahblah");
            
            // Assert
            Assert.IsFalse(isInDictionary);
        }

        [Test]
        [TestCase("all")]
        [TestCase("eat")]
        public static void IsInDictionary_WordInMemoryStream_True(string word)
        {
            // Arrange
            string[] arrayOfWords = { "all\n", "eat\n" };
            var wordsAsBytes = arrayOfWords.SelectMany(word => Encoding.ASCII.GetBytes(word)).ToArray();
            
            var engDictionary = new EnglishDictionary(new MemoryStream(wordsAsBytes));

            // Act
            var isInDictionary = engDictionary.IsInDictionary(word);

            // Assert
            Assert.IsTrue(isInDictionary);
        }

        [Test]

        public static void GetDictionaryWords_Invoked_ResultingWordsAreEqualToInputMemoryStreamWords()
        {
            // Arrange
            string[] inputWords = { "all", "eat" };
            var wordsAsBytes = inputWords.SelectMany(word => Encoding.ASCII.GetBytes(word + '\n')).ToArray();

            var engDictionary = new EnglishDictionary(new MemoryStream(wordsAsBytes));

            // Act
            var dictionaryWordsResult = engDictionary.GetDictionaryWords();

            // Assert
            Assert.AreEqual(inputWords, dictionaryWordsResult);
        }
    }
}
