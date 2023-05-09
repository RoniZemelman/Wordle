
using System.Linq;


namespace Wordle
{
    public class GuessResult  
    {
        public class GuessItem
        {
            public char letter;
            public bool isExactMatch;
            public bool isPartialMatch;

            public GuessItem(char letter, bool isExactMatch, bool isPartialMatch)
            {
                this.letter = letter;
                this.isExactMatch = isExactMatch;
                this.isPartialMatch = isPartialMatch;
            }
        }

        private GuessItem[] guessItems;

        public GuessResult()
        {
            this.guessItems = new GuessItem[5];
        }

        public void setItemAt(int index, char letter, bool isExactMatch, bool isPartialMatch)
        {
            guessItems[index] = new GuessItem(letter, isExactMatch, isPartialMatch);  // TODO: Make sure this is not double
                                                                             // instantiation of GuessItem (see ctor)
        }

        // Maybe pass in lambda to helper methods for next 2 methods
        public int GetNumExactMatches()
        {
            return guessItems.Count(g => g.isExactMatch == true);
        }

        public int GetNumPartialMatches()
        {
            return guessItems.Count(g => g.isPartialMatch == true);  
        }

    }

    
}
