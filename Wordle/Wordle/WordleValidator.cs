using System; // Char.IsLetter
using System.IO; // StreamReader
using System.Collections.Generic; // Dictionary
using System.Linq;

namespace Wordle 
{
    public class WordleValidator : IWordValidator
    {
        private static readonly string textFilePath = "C:\\Users\\user\\Desktop\\engmix\\engmix.txt";  
        private static readonly StreamReader textFileStream;  
        private static readonly Dictionary<string, bool> words;

        static WordleValidator()  
        {
            textFileStream = new StreamReader(textFilePath);
                                                              // Q: No need to manually close? CRL/GC does it?
            words = new Dictionary<string, bool>();

            ConstructMapOfFiveLetterWordsFromFile();
        }

        private static void ConstructMapOfFiveLetterWordsFromFile()
        {
            string currWord;
            while ((currWord = textFileStream.ReadLine()) != null)
            {
                if (currWord.Length == 5)
                {
                    words[currWord] = true;
                }
            }
        }

        public WordleValidator()
        {
            // Contains nothing
        }
        private bool IsValid5LetterWord(string guess)
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
                && IsValid5LetterWord(guess);
        }
    }
}
