using System; // Char.IsLetter
using System.IO; // StreamReader
using System.Collections.Generic; // Dictionary
using System.Linq;

namespace Wordle 
{
    public class WordleValidator : IWordValidator
    {
        
        public WordleValidator(IEnglishDictionary dictionary)
        {

        }

        public bool Validate(string guess)
        {
            return guess.Length == 5
                && guess.All(character => Char.IsLetter(character));
        }
    }
}
