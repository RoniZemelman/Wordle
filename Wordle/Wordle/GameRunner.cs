using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class GameRunner
    {
        private WordleGame wordleGame;
        public GameRunner(WordleGame wordleGame)
        {
            this.wordleGame = wordleGame;
        }

        public void EnterUserGuess(string userGuess)
        {
            var validatorResult = new GuessValidator.ValidatorResult();
            wordleGame.PlayTurn(userGuess, out validatorResult);
        }
    
        public bool UserIsAlive()
        {
            return wordleGame.Status() == WordleGame.State.IsAlive;
        }

        public bool UserWon()
        {
            return wordleGame.Status() == WordleGame.State.Won;
        }
    }
}
