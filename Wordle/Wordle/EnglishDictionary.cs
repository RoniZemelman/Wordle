using System.Collections.Generic;
using System.IO;

namespace Wordle
{
    public class EnglishDictionary : IEnglishDictionary
    {
        private readonly StreamReader _streamReader;
        //private Dictionary<string, bool> _mapOfWords; // TODO Use HashSet
        private HashSet<string> _mapOfWords;

        public EnglishDictionary(string filePath)
        {
            _streamReader = new StreamReader(filePath);
            
            ConstructMapOfWords();

            _streamReader.Close();
        }

        private void ConstructMapOfWords()
        {
            _mapOfWords = new HashSet<string>();
            string word;

            while ((word = _streamReader.ReadLine()) != null)
            { 
                _mapOfWords.Add(word);
            }
        }

        public bool IsInDictionary(string word)
        {
            return _mapOfWords.Contains(word);
        }
    }


}
