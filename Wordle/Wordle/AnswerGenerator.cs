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
        private readonly GuessValidator _guessValidator;
        private readonly Random _randomizer; 



        public AnswerGenerator(EnglishDictionary engDictionary)
        {
            _guessValidator = new GuessValidator(engDictionary); // TODO remove and inject
            _answers = CreateAnswersArray(engDictionary);

            _randomizer = new Random();
        }

        private string[] CreateAnswersArray(EnglishDictionary engDictionary)
        {
            var dictionaryWords = engDictionary.GetDictionaryWords();
            
            return dictionaryWords
                .Where(word =>
                {
                    var validatorResult = _guessValidator.Validate(word);
                    return validatorResult.IsValidGuess();
                }) 
                .ToArray();
        }

        public int NumOfAnswers()
        {
            return _answers.Length;
        }

        public string GenerateAnswer()
        {
            var randomIndex = _randomizer.Next(NumOfAnswers());

            return _answers[randomIndex]; // TODO: null when empty
        }
    }
}
