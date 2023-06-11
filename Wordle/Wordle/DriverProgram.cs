
using System;

namespace Wordle
{
    class DriverProgram
    {
        private static WordleGame CreateGame()
        {
            var guessAnalyzer = new GuessAnalyzer("bingo");

            // TODO find better dictionary
            var engDictionary = new EnglishDictionary("C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt.txt");
            var guessValidator = new GuessValidator(engDictionary);

            return new WordleGame(guessAnalyzer, guessValidator);
        }

        private static void PrintValidationResult(string guess, GuessResult guessResult)
        {
            if (!guessResult.ValidationResult.Is5Letters)
                Console.WriteLine($"'{guess}' is not 5 letters");

            if (!guessResult.ValidationResult.IsAllChars)
                Console.WriteLine($"'{guess}' is not all characters");

            if (!guessResult.ValidationResult.IsInDictionary)
                Console.WriteLine($"'{guess}' is not in dictionary.");
        }
        private static void DisplayGuessResultAnalysis(GuessResult guessResult)
        {
            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                var guessLetter = guessResult.At(i);

                if (guessLetter.IsExactMatch())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(guessLetter.Letter);
                    continue;
                }

                if (guessLetter.IsPartialMatch())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(guessLetter.Letter);
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(guessLetter.Letter);

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WordleGame!  Please enter a 5 letter word.");

            var wordleGame = CreateGame();

            RunGame(wordleGame);
        }

        private static void RunGame(WordleGame wordleGame)
        {
            while (wordleGame.Status() == WordleGame.State.IsAlive)
            {
                Console.Write("\nTurns remaining: " + wordleGame.TurnsRemaining()
                                                    + "\nPlease enter a guess: ");
                var guess = Console.ReadLine();

                var guessResult = wordleGame.PlayTurn(guess);

                if (!guessResult.IsValid())
                {
                    PrintValidationResult(guess, guessResult);
                    continue;
                }

                DisplayGuessResultAnalysis(guessResult);
            }

            if (wordleGame.Status() == WordleGame.State.Won)
            {
                Console.WriteLine("Congrats! You won!");
                return;
            }

            Console.WriteLine("Sorry! You lost.");
        }
    }
}
