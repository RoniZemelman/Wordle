using System.IO;

namespace Wordle
{
    public class EnglishDictionary : IEnglishDictionary
    {
        private StreamReader _streamReader;

        public EnglishDictionary(string filePath)
        {
            _streamReader = new StreamReader(filePath);
        }

        public bool IsInDictionary(string word)
        {
            return false;
        }
    }


}
