
using System;
using static Wordle.GuessValidator;

namespace Wordle 
{
    public interface IWordValidator
    {
        ValidatorResult Validate(string guess);
    }

}
