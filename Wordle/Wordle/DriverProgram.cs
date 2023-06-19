
using System;
using System.IO;
using System.Linq;

namespace Wordle
{
    class DriverProgram
    {
        private static readonly string DictionaryFilePath = 
            "C:\\Users\\user\\source\\repos\\Wordle\\WordleTests2\\10000words.txt";

        private static string _currentAnswer; // TODO thread safety

        private static WordleGame CreateGame()
        {
            var fileContents = File.ReadAllBytes(DictionaryFilePath);
            var engDictionary = new EnglishDictionary(new MemoryStream(fileContents));
            var validator = new GuessValidator(engDictionary);
            var answerGenerator = new AnswerGenerator(engDictionary);

            _currentAnswer = answerGenerator.GenerateAnswer(); 

            return new WordleGame(_currentAnswer, validator);
        }

        private static void DisplayValidationErrors(string guess, GuessResult guessResult)
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
            for (int i = 0; i < guessResult.Length(); ++i) 
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


        private static void RunGame(WordleGame wordleGame)
        {
            while (wordleGame.Status() == WordleGame.State.IsRunning)
            {
                Console.Write("\nTurns remaining: " + wordleGame.TurnsRemaining()
                                                    + "\nPlease enter a guess: ");
                var guess = Console.ReadLine();

                var guessResult = wordleGame.PlayTurn(guess);

                if (!guessResult.IsValid())
                {
                    DisplayValidationErrors(guess, guessResult);
                    continue;
                }

                DisplayGuessResultAnalysis(guessResult);
            }

            DisplayGameResult(wordleGame);
        }

        private static void DisplayGameResult(WordleGame wordleGame)
        {
            if (wordleGame.Status() == WordleGame.State.Won)
            {
                Console.WriteLine("Congrats! You won!");
                return;
            }

            Console.WriteLine($"Sorry! You lost. Correct Answer: {_currentAnswer}");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WordleGame! " +
                              $" Please enter a {WordleGame.NumLettersInWord} letter word.");

            DisplayKey();

            var wordleGame = CreateGame();
            RunGame(wordleGame);
        }

        private static void DisplayKey()
        {
            Console.WriteLine("\n\nCOLOR KEY");
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nGreen:  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("letter is correct (in correct location)");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Yellow:  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("letter is correct (in correct location)");


            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Red:  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("letter is not in the answer.");
        }
    }
}
