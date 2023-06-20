using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class AnswerGenerator
    {
        private readonly string[] _answers;

        // Overload ctor with randomizer? 

        public AnswerGenerator(IWordValidator validator, string[] dictionaryWords)
        {
            _answers = dictionaryWords.Where(word => validator.Validate(word).IsValidGuess()).ToArray();
        }

        public string GenerateAnswer()
        {
            if (_answers.Length == 0)
                return null;

            return _answers[0];
        }
    }
}
