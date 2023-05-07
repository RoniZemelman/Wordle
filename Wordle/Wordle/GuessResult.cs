using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class GuessResult
    {
        public class GuessItem
        {
            public char letter;
            public bool isExactMatch;
            public bool isInWord;

            public GuessItem(char letter, bool isExactMatch, bool isInWord)
            {
                this.letter = letter;
                this.isExactMatch = isExactMatch;
                this.isInWord = isInWord;
            }
        }

        private GuessItem[] guessItems;

        public GuessResult()
        {
            this.guessItems = new GuessItem[5];
        }

        public void setItemAt(int index, char letter, bool isExactMatch, bool isInWord)
        {
            guessItems[index] = new GuessItem(letter, isExactMatch, isInWord);  // TODO: Make sure this is not double
                                                                             // instantiation of GuessItem (see ctor)
        }

        public int GetNumExactMatches()
        {
            return this.guessItems.Count(g => g.isExactMatch == true);
        }

        public int GetNumPartialMatches()
        {
            return 0; 
            // this.guessItems.Count(g => g.isInWord == true); // TODO - disregard previous exact matches
        }

    }

    
}
