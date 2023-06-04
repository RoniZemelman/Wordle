using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static WordleTests.WordleGameTests;
using static Wordle.GuessResult;

namespace WordleTests
{
    class GameRunnerTests
    {
        public static IGuessAnalyzer CreateMockGuessAnalyzerReturnsCorrect()
        {
            var mockGuessAnalzer = MockRepository.GenerateStub<IGuessAnalyzer>();

            GuessResult winningGuessResult = new GuessResult();

            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                winningGuessResult.SetItemAt(i, new GuessItem('*', isExactMatch: true, 
                                                                        isPartialMatch: false ));
            }

            mockGuessAnalzer.Stub(g => g.Analyze("")).IgnoreArguments().Return(winningGuessResult);

            return mockGuessAnalzer;
        }


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

            var correctUserGuess = "doesntMatterWillBeAcceptedasCorrectAnswer";

            // Act
            gameRunner.EnterUserGuess(correctUserGuess);
            
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
            gameRunner.EnterUserGuess(enteredUserGuess);

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

            var incorrectUserGuess = "IncorrectGuessDontCare";

            // Act
            for (int turn = 0; turn < WordleGame.MaxNumOfTurns; ++turn)
            {
                gameRunner.EnterUserGuess(incorrectUserGuess);
            }

            Assert.IsTrue(gameRunner.UserLost());
        }

        // TODO  -
        // 1) user starts with 5 turns, make sure they're updating?
        // Already tested functionality though in WordleGame...
        // 2) User attempts to enterGuess when not Alive (won/lost) -> expected behavior
        // could expect exception throughn, simply rejected (no update to state, handled with conditional logic
        // in EnterGuess?
    }
}
