using System;
using System.IO;
 using System.Linq;
using System.Text;
 
namespace Wordle // Maybe move IWordValidator to own namespace, should work for other games
{
    interface IWordValidator
    {
        bool Validate(string guess);
    }
    

    // TODO move to own file
    public class WordleValidator : IWordValidator
    {
        private StreamReader textFileStream;
        public WordleValidator()
        {
            textFileStream = new StreamReader("C:\\Users\\user\\Desktop\\engmix\\engmix.txt");
            
            // NOTE: Need a better dictionary, maybe shorten it for only 5 letter words
            // TODO put words in data struct - map (Refactor part)
            // TODO where to close the file? CLR/GC takes care of it?

            //Sanity check
            //string line;
            //int counter = 0;
            //while ((line = textFileStream.ReadLine()) != null)
            //{
            //    Console.WriteLine(counter + ": " + line);
            //    ++counter;
            //}

        }

        private bool IsEnglishWord(string guess)
        {
            string dictWord;

            while ((dictWord = textFileStream.ReadLine()) != null)
            {
                if (dictWord == guess)
                    return true;
            }

            return false;
        }

        public bool Validate(string guess)
        {
            return guess.Length == 5 
                && guess.All(character => Char.IsLetter(character))
                && IsEnglishWord(guess);
        }
    }

}
