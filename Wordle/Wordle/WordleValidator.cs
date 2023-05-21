using System; // Char.IsLetter
using System.Linq;

namespace Wordle 
{
    public class WordleValidator : IWordValidator
    {
        public class ValidatorResult
        {
            private readonly bool is5Letters;
            private readonly bool isAllChars;
            private readonly bool isInDictionary;
            public ValidatorResult(bool is5Letters = false, 
                                   bool isAllChars = false, 
                                   bool isInDictionary = false)
            {
                this.is5Letters = is5Letters;
                this.isAllChars = isAllChars;
                this.isInDictionary = isInDictionary;
            }
            
            public bool Is5Letters
            { get;  }

            public bool IsAllChars
            { get; }

            public bool IsInDictionary
            { get; }

        }

        readonly IEnglishDictionary dictionary; // Non-interface?
        
        public WordleValidator(IEnglishDictionary dictionary)
        {
            this.dictionary = dictionary;
        }

        public object Validate(string guess) // returns object since did not want
                                             // IWordValidator to be bound to WordleValidator result. Maybe return interface type?
        {
            var is5Letters = (guess.Length == 5);
            var isAllChars = guess.All(character => Char.IsLetter(character));
            var isInDictionary = dictionary.IsInDictionary(guess);

            return new ValidatorResult(is5Letters, isAllChars, isInDictionary);
        }
    }
}
