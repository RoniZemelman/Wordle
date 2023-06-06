using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static WordleTests.Utils;

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

            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);

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

            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsCorrect();
            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            var gameRunner = new GameRunner(wordleGame);

            var correctUserGuess = "willBeAcceptedasCorrectAnswer";

            // Act
            gameRunner.AcceptUserGuess(correctUserGuess);

            // Assert
            Assert.IsTrue(gameRunner.UserWon());
        }
       
        [Test]
        public static void AcceptUserGuess_UserEnteredIncorrectGuess_UserIsAlive()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(true, true, true));

            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect(); // static method in WordleGameTests, TODO: refactor

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            var gameRunner = new GameRunner(wordleGame);

            var enteredUserGuess = "incorrect";

            // Act
            gameRunner.AcceptUserGuess(enteredUserGuess);

            // Assert
            Assert.IsTrue(gameRunner.UserIsAlive());
            Assert.IsFalse(gameRunner.UserWon());
        }

        [Test]
        public static void AcceptUserGuess_UserEntered5IncorrectGuess_UserLost()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(true, true, true));

            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect(); 

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            var gameRunner = new GameRunner(wordleGame);

            var incorrectUserGuess = "anIncorrectGuess";

            // Act
            for (int turn = 0; turn < WordleGame.MaxNumOfTurns; ++turn)
            {
                gameRunner.AcceptUserGuess(incorrectUserGuess);
            }

            // Assert
            Assert.IsTrue(gameRunner.UserLost());
        }
        [Test] // TODO 
        public static void AcceptUserGuess_UserAttemptsGuessWhenNotAlive_EXPECTED_BEHAVIOR_TO_BE_DETERMINED()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(true, true, true));

            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            var gameRunner = new GameRunner(wordleGame);

            var incorrectUserGuess = "anIncorrectGuess";

            // Act
            for (int turn = 0; turn < WordleGame.MaxNumOfTurns + 1; ++turn)
            {
                gameRunner.AcceptUserGuess(incorrectUserGuess);
            }

            // Assert
            //Assert.IsFalse(true);  // Options: exception thrown by GameRunner,
                                   // return null or false in AcceptUserGuess, maybe WordleGame responsibility ... 
        }

        // TODO  -
        // 1) Expect GameRunner.UserTurnsRemaining()? Already tested functionality though in WordleGame...
        // 2) User attempts to enterGuess when not Alive (won/lost) -> expected behavior
        // could expect exception throughn, simply rejected (no update to state, handled with conditional logic
        // in EnterGuess?
        // 3) Expect Guess Result and validator result in GameRunner (have them as fields? currGuessResult, currValidatorResult)
    }
}
