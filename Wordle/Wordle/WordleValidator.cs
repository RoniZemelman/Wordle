using System; // Char.IsLetter
using System.Linq;

namespace Wordle 
{
    public class WordleValidator : IWordValidator
    {
        IEnglishDictionary dictionary; // Non-interface?
        
        public WordleValidator(IEnglishDictionary dictionary)
        {
            this.dictionary = dictionary;
        }

        public bool Validate(string guess)
        {
            return guess.Length == 5
                && guess.All(character => Char.IsLetter(character))
                && dictionary.IsInDictionary(guess);
        }
    }
}
