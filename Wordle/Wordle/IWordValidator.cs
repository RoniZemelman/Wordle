
using System;

namespace Wordle // Maybe move IWordValidator to own namespace, should work for other contexts besides Wordle
{
    public interface IWordValidator
    {
        object Validate(string guess);
    }

}
