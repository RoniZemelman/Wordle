
using System.Linq;


namespace Wordle
{
    public class GuessResult  
    {
        public class GuessItem
        {
            public char _letter;
            public bool _isExactMatch;
            public bool _isPartialMatch;

            public GuessItem(char letter, bool isExactMatch = false, bool isPartialMatch = false)
            {
                _letter = letter;
                _isExactMatch = isExactMatch;
                _isPartialMatch = isPartialMatch;
            }

            public bool IsExactMatch() { return _isExactMatch; }

            public bool IsPartialMatch() { return _isPartialMatch; }

            public bool CompleteMiss() { return !_isExactMatch && !_isPartialMatch; }
        }

        private GuessItem[] _guessItems;
        public GuessResult()
        {
            _guessItems = new GuessItem[WordleGame.NumLettersInWord];

            // guessItems array lazily constructed when needed...Pros/Cons?
        }
        
        public void SetItemAt(int index, GuessItem guessItem)
        {
            _guessItems[index] = guessItem;
        }                                                                               

        public bool IsCorrectGuess()
        {
            return GetNumExactMatches() == WordleGame.NumLettersInWord;
        }

        public int GetNumExactMatches()
        {
            return _guessItems.Count(g => g.IsExactMatch() == true);
        }

        public int GetNumPartialMatches()
        {
            return _guessItems.Count(g => g.IsPartialMatch() == true);  
        }

        public GuessItem At(int index)
        {
            return _guessItems[index];
        }
    }

    
}
