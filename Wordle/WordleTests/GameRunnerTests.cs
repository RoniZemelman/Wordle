using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static WordleTests.WordleGameTests;

namespace WordleTests
{
    class GameRunnerTests
    {
        [Test]
        public static void Constructor_GameRunnerConstructed_UserIsAlive()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(true, true, true));

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            var gameRunner = new GameRunner(wordleGame);

            // Assert
            Assert.IsTrue(gameRunner.UserIsAlive());  
        }

        [Test]
        public static void AcceptUserGuess_UserEnteredCorrectGuess_UserWon()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(true, true, true));

            var answer = "bingo";
            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            var gameRunner = new GameRunner(wordleGame);

            var enteredUserGuess = answer;

            // Act
            gameRunner.EnterUserGuess(enteredUserGuess);
            
            Assert.IsTrue(gameRunner.UserWon());
        }

        [Test]
        public static void AcceptUserGuess_UserEnteredIncorrectGuess_UserIsAlive()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(true, true, true));

            var mockGuessAnalyzer = CreateMockGuessAnalyzer(); // static method in WordleGameTests, TODO: refactor

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            var gameRunner = new GameRunner(wordleGame);

            var enteredUserGuess = "incorrect";

            // Act
            gameRunner.EnterUserGuess(enteredUserGuess);

            Assert.IsTrue(gameRunner.UserIsAlive());
            Assert.IsFalse(gameRunner.UserWon());
        }


    }
}
