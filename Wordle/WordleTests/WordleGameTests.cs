using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;

namespace WordleTests
{
    class WordleGameTests
    { 
        [Test]
        public static void Constructor_WordleGameCreated_MaxNumTurnsRemaining()
        {
            // Arrange            
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments().Return(null);  
            
            // Act
            var wordleGame = new WordleGame(mockValidator);

            // Assert
            Assert.AreEqual(WordleGame.MaxNumOfTurns, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void Constructor_WordleGameCreated_StatusIsRunning()
        {
            // Arrange
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments().Return(null);

            // Act
            var wordleGame = new WordleGame(mockValidator);

            // Assert
            Assert.AreEqual(wordleGame.Status(), WordleGame.state.IsRunning);
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_RemainingTurnsUnchanged()
        {
            // Arrange
            string userGuess = "blablah"; // TODO - pass in answer to WordleGame?

            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess))
                .IgnoreArguments()
                .Return(new ValidatorResult(false, false, false));

            var wordleGame = new WordleGame(mockValidator);
            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            //act
            wordleGame.PlayTurn(userGuess);

            // assert
            Assert.AreEqual(initialTurnsRemaining, wordleGame.TurnsRemaining());
        }

        [Test]
        [TestCase("valid")]
        [TestCase("NotValid")]
        public static void PlayTurn_PlayTurnInvokedWithUserGuess_ValidatorIsCalled(string userGuess)
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(null); 

            var wordleGame = new WordleGame(mockValidator);

            // Act
            wordleGame.PlayTurn(userGuess);

            // assert
            mockValidator.AssertWasCalled(v => v.Validate(userGuess));
        }
        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_ReturnsGuessResultObject()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(new ValidatorResult(true));

            var wordleGame = new WordleGame(mockValidator);

            // Act
            var resultOfGuess = wordleGame.PlayTurn(userGuess);

            // Assert
            Assert.NotNull(resultOfGuess);
        }

        // Test PlayTurn returns null when validator invalidates guess
        // Test PlayTurn that validatorResult is returned (outparam?) when validator validates
    }
}
