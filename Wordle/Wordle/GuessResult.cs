
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

            public GuessItem(char letter, bool isExactMatch = false, bool isPartialMatch = false)
            {
                this.letter = letter;
                this.isExactMatch = isExactMatch;
                this.isPartialMatch = isPartialMatch;
            }

            public bool IsExactMatch() { return isExactMatch; }

            public bool IsPartialMatch() { return isPartialMatch; }

            public bool CompleteMiss() { return !isExactMatch && !isPartialMatch; }
        }

        private GuessItem[] guessItems;
        public GuessResult()
        {
            this.guessItems = new GuessItem[WordleGame.NumLettersInWord];

            // guessItems array lazily constructed when needed...Pros/Cons?
        }
        
        public void setItemAt(int index, char letter, bool isExactMatch, bool isPartialMatch)
        {
            guessItems[index] = new GuessItem(letter, isExactMatch, isPartialMatch);  // TODO: Make sure this is not double
                                                                                     // instantiation of GuessItem (see ctor)
        }

        public bool IsCorrect()
        {
            return GetNumExactMatches() == WordleGame.NumLettersInWord;
        }

        public int GetNumExactMatches()
        {
            return guessItems.Count(g => g.IsExactMatch() == true);
        }

        public int GetNumPartialMatches()
        {
            return guessItems.Count(g => g.IsPartialMatch() == true);  
        }

        public GuessItem At(int index)
        {
            return guessItems[index];
        }
    }

    
}
