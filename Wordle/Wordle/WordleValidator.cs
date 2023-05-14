using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Wordle
{
    // TODO move to own file
    public class WordleValidator : IWordValidator
    {
        private static string textFilePath = "C:\\Users\\user\\Desktop\\engmix\\engmix.txt"; // Note: change to your filepath
        private StreamReader textFileStream;
        private Dictionary<string, bool> words;
        public WordleValidator()
        {
            textFileStream = new StreamReader(textFilePath); 

            words = new Dictionary<string, bool>();

            ConstructMapOfFiveLetterWordsFromFile();

            textFileStream.Close(); // Question to clarify: Is this needed? CLR/GC takes care of it?
        }

        private void ConstructMapOfFiveLetterWordsFromFile()
        {
            string currWord;
            while ((currWord = textFileStream.ReadLine()) != null)
            {
                if (currWord.Length == 5) { words[currWord] = true; }
            }
        }

        private bool IsEnglishWord(string guess)
        {
            try
            {
                return words[guess];
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public bool Validate(string guess)
        {
            return guess.Length == 5 
                && guess.All(character => Char.IsLetter(character))
                && IsEnglishWord(guess);
        }
    }

}
