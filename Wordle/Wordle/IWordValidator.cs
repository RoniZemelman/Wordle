using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle // Maybe move IWordValidator to own namespace, should work for other games
{
    interface IWordValidator
    {
        bool Validate(string guess);
    }
    

    // TODO move to own file
    public class WordleValidator : IWordValidator
    {
        public WordleValidator()
        {

        }

        public bool Validate(string guess)
        {
            return guess.Length == 5;
        }
    }

}
