using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;

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

        public static void AcceptUserGuess_CorrectUserGuessEntered_IsWon()
        {
            //// Arrange 
            //var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            //var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            //mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
            //    .Return(new ValidatorResult(true, true, true)); 
            
            //var wordleGame = new WordleGame(new GuessAnalyzer("bingo"), mockValidator);
            //var gameRunner = new GameRunner(wordleGame);

            //var enteredUserGuessDontCare = "dontCare";

            //// Act
            //gameRunner.AcceptUserGuess(enteredUserGuessDontCare);

            //Assert.IsTrue(gameRunner.IsRunning());
        }

       

    }
}
