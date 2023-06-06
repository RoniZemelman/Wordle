using System; // Char.IsLetter
using System.Linq;

namespace Wordle 
{
    public class GuessValidator : IWordValidator
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
            { get => is5Letters;  }

            public bool IsAllChars
            { get => isAllChars; }

            public bool IsInDictionary
            { get => isInDictionary; }

            public bool IsValidGuess()
            {
                return is5Letters && isAllChars && isInDictionary;
            }
        }

        readonly IEnglishDictionary dictionary; // Non-interface?
        
        public GuessValidator(IEnglishDictionary dictionary)
        {
            this.dictionary = dictionary;
        }

        public ValidatorResult Validate(string guess) // returns object since did not want
                                             // IWordValidator to be bound to WordleValidator result.
                                             // Maybe return interface type?
        {
            return new ValidatorResult(guess.Length == 5,
                                        guess.All(character => Char.IsLetter(character)),
                                        dictionary.IsInDictionary(guess));
        }
    }
}
