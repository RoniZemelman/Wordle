using System; // Char.IsLetter
using System.Linq;

namespace Wordle 
{
    public class GuessValidator : IWordValidator
    {
        public class ValidatorResult 
        {
            private readonly bool _is5Letters;
            private readonly bool _isAllChars;
            private readonly bool _isInDictionary;
            public ValidatorResult(bool is5Letters = false, 
                                   bool isAllChars = false, 
                                   bool isInDictionary = false)
            {
                _is5Letters = is5Letters;
                _isAllChars = isAllChars;
                _isInDictionary = isInDictionary;
            }
            
            public bool Is5Letters
            { get => _is5Letters;  }

            public bool IsAllChars
            { get => _isAllChars; }

            public bool IsInDictionary
            { get => _isInDictionary; }

            public bool IsValidGuess()
            {
                return _is5Letters && _isAllChars && _isInDictionary;
            }

            public int ErrorCount()
            {
                return Convert.ToInt32(!_is5Letters)
                        + Convert.ToInt32(!_isAllChars)
                        + Convert.ToInt32(!_isInDictionary);
            }
        }

        private readonly IEnglishDictionary _dictionary; 
        
        public GuessValidator(IEnglishDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public ValidatorResult Validate(string guess) // returns object since did not want
                                             // IWordValidator to be bound to WordleValidator result.
                                             // Maybe return interface type?
        {
            return new ValidatorResult(guess.Length == 5,
                                        guess.All(character => Char.IsLetter(character)),
                                        _dictionary.IsInDictionary(guess));
        }
    }
}
