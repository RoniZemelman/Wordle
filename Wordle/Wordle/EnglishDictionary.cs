using System;
using System.Collections.Generic;
using System.IO;

namespace Wordle
{
    public class EnglishDictionary : IEnglishDictionary
    {
        private StreamReader _streamReader;
        private Dictionary<string, bool> _mapOfWords;

        public EnglishDictionary(string filePath)
        {
            _streamReader = new StreamReader(filePath);
            
            ConstructMapOfWords();

            _streamReader.Close();
        }

        private void ConstructMapOfWords()
        {

            _mapOfWords = new Dictionary<string, bool>();
            
            string word = _streamReader.ReadLine();

            while (word != null)
            { 
                _mapOfWords.Add(word, true);

                word = _streamReader.ReadLine();
            }
        }

        public bool IsInDictionary(string word)
        {
            return false;
        }
    }


}
