using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Wordle
{
    public class EnglishDictionary : IEnglishDictionary
    {
        private readonly StreamReader _streamReader;
        private HashSet<string> _setOfWords;

        public EnglishDictionary(MemoryStream memoryStream)
        {
            _streamReader = new StreamReader(memoryStream);
            
            ConstructMapOfWords();

            _streamReader.Close();
        }

        private void ConstructMapOfWords()
        {
            _setOfWords = new HashSet<string>();
            string word;

            while ((word = _streamReader.ReadLine()) != null)
            { 
                _setOfWords.Add(word);
            }
        }

        // TODO performance considerations of ToArray?
        public IEnumerable<string> GetDictionaryWords()
        {
            return _setOfWords.ToArray();
        }

        public bool IsInDictionary(string word)
        {
            return _setOfWords.Contains(word);
        }
    }


}
