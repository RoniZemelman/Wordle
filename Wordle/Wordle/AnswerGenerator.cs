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

        private readonly Random _randomizer;

        // Overload ctor with randomizer? 

        public AnswerGenerator(IWordValidator validator, string[] dictionaryWords)
        {
            _answers = dictionaryWords.Where(word => validator.Validate(word).IsValidGuess()).ToArray();
            _randomizer = new Random();
        }

        public string GenerateAnswer()
        {
            if (_answers.Length == 0)
                return null;

            var nextIndex = _randomizer.Next(_answers.Length);

            return _answers[nextIndex];
        }
    }
}
