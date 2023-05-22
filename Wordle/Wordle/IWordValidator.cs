
using System;
using static Wordle.WordleValidator;

namespace Wordle 
{
    public interface IWordValidator
    {
        ValidatorResult Validate(string guess);
    }

}
