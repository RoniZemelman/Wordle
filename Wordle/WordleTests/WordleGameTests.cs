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
            Assert.AreEqual(WordleGame.State.IsRunning, wordleGame.Status());
        }

        [Test]
        [TestCase("valid")]
        [TestCase("notValid")]
        public static void PlayTurn_PlayTurnInvoked_ValidatorIsCalled(string userGuess)
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate("valid")).Return(new ValidatorResult(true, true, true));
            mockValidator.Stub(d => d.Validate("notValid")).Return(new ValidatorResult(false, false, false));

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(mockValidator);
            
            // Act
            wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            mockValidator.AssertWasCalled(v => v.Validate(userGuess));
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_RemainingTurnsUnchanged()
        {
            // Arrange
            string userGuess = "blablah"; 
            // Need to pass in answer to WordleGame?

            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess))
                .IgnoreArguments()
                .Return(new ValidatorResult(false, false, false));

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(mockValidator);

            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            // Act
            wordleGame.PlayTurn(userGuess, out dontCare);

            // assert
            Assert.AreEqual(initialTurnsRemaining, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_RemainingTurnsDecrementedByOne()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(new ValidatorResult(true, true, true));

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(mockValidator);

            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            // Act
            wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            Assert.AreEqual(initialTurnsRemaining - 1, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_GuessResultObjectReturned()
        {
            // Arrange
            var userGuess = "valid";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(new ValidatorResult(true, true, true));

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(mockValidator);

            // Act
            var playTurnResult =  wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            Assert.IsInstanceOf(typeof(GuessResult), playTurnResult);
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_NullReturned()
        {
            // Arrange
            var userGuess = "Invalid";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(new ValidatorResult(false, false, false));

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(mockValidator);

            // Act
            var playTurnResult = wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            Assert.IsNull(playTurnResult);
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesGuess_ValidatorOutParamIsValid()
        {
            // Arrange
            var userGuess = "valid";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(new ValidatorResult(true, true, true));

            var wordleGame = new WordleGame(mockValidator);

            var validatorResult = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(userGuess, out validatorResult);

            // Assert
            Assert.IsTrue(validatorResult.IsValidGuess());
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesGuess_ValidatorOutParamIsNotValid()
        {
            // Arrange
            var userGuess = "Invalid";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(userGuess)).Return(new ValidatorResult(false, false, false));

            var wordleGame = new WordleGame(mockValidator);

            var validatorResult = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(userGuess, out validatorResult);

            // Assert
            Assert.IsFalse(validatorResult.IsValidGuess());
        }

        [Test] // Maybe Change - Granularity test for validator object
        public static void PlayTurn_ValidatorInvalidsFor1Reason_ValidatorOutputHasOnlyOneFalseField()
        {
            // Arrange
            var guessNot5Letters = "TooLong";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(guessNot5Letters))
                .Return(new ValidatorResult(is5Letters:false,
                                            isAllChars: true, isInDictionary:true));

            var wordleGame = new WordleGame(mockValidator);

            var validatorResult = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(guessNot5Letters, out validatorResult);

            // Assert
            Assert.IsFalse(validatorResult.Is5Letters); // Remove?
            Assert.IsTrue(validatorResult.IsAllChars && validatorResult.IsInDictionary);
        }
        
        // Test - StatusWins when UserGuess Has Exact Match
        // Test - Status Lost when UserGuess 5 times without exact match

    }
}
