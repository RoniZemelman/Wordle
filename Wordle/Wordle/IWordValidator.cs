using System.Text;

namespace Wordle // Maybe move IWordValidator to own namespace, should work for other contexts besides Wordle
{
    interface IWordValidator
    {
        bool Validate(string guess);
    }

}
