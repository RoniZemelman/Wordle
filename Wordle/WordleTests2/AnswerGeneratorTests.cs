﻿using System;
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
        private const string TestingDictionaryFilePath = "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt";
        private static byte[] _fileContents;

        [OneTimeSetUp]
        public static void ReadFileContentsIntoMainMemory()
        {
            _fileContents = File.ReadAllBytes(TestingDictionaryFilePath);
        }

        [Test] // Remove 
        public static void Constructor_AnswerGeneratorCreated_NotNull()
        {
            // Arrange
            var memoryStream = new MemoryStream(_fileContents);

            // Act
            var answerGenerator = new AnswerGenerator(new EnglishDictionary(memoryStream));

            // Assert
            Assert.IsNotNull(answerGenerator);
        }
    }
}
