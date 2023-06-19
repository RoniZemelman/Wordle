﻿using System;
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
        [TestCase("blahblah")]
        [TestCase("shtuyot")]
        public static void IsInDictionary_WordNotInDictionary_False(string wordNotInDictionary)
        {
            // Arrange
            string[] arrayOfWords = {"all\n", "eat\n"};
            var wordsAsBytes = arrayOfWords.SelectMany(word => Encoding.ASCII.GetBytes(word)).ToArray();

            var engDictionary = new EnglishDictionary(new MemoryStream(wordsAsBytes)); // _fileContents));

            // Act
            var isInDictionary = engDictionary.IsInDictionary(wordNotInDictionary);
            
            // Assert
            Assert.IsFalse(isInDictionary);
        }

        [Test]
        [TestCase("all")]
        [TestCase("eat")]
        public static void IsInDictionary_RealWordFromDictionaryFile_True(string word)
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
    }
}
