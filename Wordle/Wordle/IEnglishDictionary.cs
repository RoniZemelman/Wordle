using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public interface IEnglishDictionary
    {
        bool IsInDictionary(string word);
    }


}
