﻿
namespace Wordle // Maybe move IWordValidator to own namespace, should work for other contexts besides Wordle
{
    public interface IWordValidator
    {
        bool Validate(string guess);
    }

}
