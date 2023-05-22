

using System;
using Wordle;
using static Wordle.WordleValidator;

namespace Wordle
{
    public class WordleGame
    {
        public const int MaxNumOfTurns = 5;

        private readonly IWordValidator validator;
        public WordleGame(IWordValidator validator)
        {
            this.validator = validator;
        }
    
        public int TurnsRemaining()
        {
            return 5;
        }
        public void PlayTurn(string userGuess)
        {
            // TODO 
        }
    }
}
