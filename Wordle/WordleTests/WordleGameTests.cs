using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static Wordle.GuessResult;

namespace WordleTests
{
    class WordleGameTests
    { 
        private static IWordValidator CreateAndConfigureMockValidator(bool validatorValue)
        {
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments()
                .Return(new ValidatorResult(validatorValue, validatorValue, validatorValue));

            return mockValidator;
        }

        public static IGuessAnalyzer CreateMockGuessAnalyzerReturnsIncorrect()
        {
            var mockGuessAnalzer = MockRepository.GenerateStub<IGuessAnalyzer>();
         
            GuessResult mockGuessResultDontCare = new GuessResult();
            const char dontCare = '*';

            for (int i = 0; i < WordleGame.NumLettersInWord; ++i)
            {
                mockGuessResultDontCare.SetItemAt(i, new GuessItem(dontCare));
            }

            mockGuessAnalzer.Stub(g => g.Analyze("")).IgnoreArguments().Return(mockGuessResultDontCare);

            return mockGuessAnalzer;
        }

        [Test]
        public static void Constructor_WordleGameCreated_MaxNumTurnsRemaining()
        {
            // Arrange            
            var mockValidator = CreateAndConfigureMockValidator(false);
            
            // Act
            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Assert
            Assert.AreEqual(WordleGame.MaxNumOfTurns, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void Constructor_WordleGameCreated_StatusIsAlive()
        {
            // Arrange
            var mockValidator = CreateAndConfigureMockValidator(false);

            // Act
            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Assert
            Assert.AreEqual(WordleGame.State.IsAlive, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_RemainingTurnsUnchanged()
        {
            // Arrange
            string userGuess = "InvalidGuess";

            var mockValidator = CreateAndConfigureMockValidator(false);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            var dontCare = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(userGuess, out dontCare);

            // assert
            Assert.AreEqual(initialTurnsRemaining, wordleGame.TurnsRemaining());
        }



        [Test]
        [TestCase("valid")]
        [TestCase("notValid")]
        public static void PlayTurn_PlayTurnInvoked_ValidatorIsAlwaysCalled(string userGuess)
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate("valid")).Return(new ValidatorResult(true, true, true));
            mockValidator.Stub(d => d.Validate("notValid")).Return(new ValidatorResult(false, false, false));

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);
            
            // Act
            wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            mockValidator.AssertWasCalled(v => v.Validate(userGuess));
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_GuessAnalyzerIsNotCalled()
        {
            // Arrange 
            var userGuessNotValidated = "NotValid";
            var mockInvalidatingValidator = CreateAndConfigureMockValidator(false);

            var mockGuessAnalzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalzer, mockInvalidatingValidator);

            var dontCare = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(userGuessNotValidated, out dontCare);

            // Assert
            mockGuessAnalzer.AssertWasNotCalled(v => v.Analyze(userGuessNotValidated));
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_GuessAnalyzerIsCalled()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidatingValidator = CreateAndConfigureMockValidator(true);

            var mockGuessAnalzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalzer, mockValidatingValidator);

            var dontCare = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            mockGuessAnalzer.AssertWasCalled(v => v.Analyze(userGuess));
        }


        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_RemainingTurnsDecrementedByOne()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);
            
            var dontCare = new ValidatorResult();

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
            var mockValidator = CreateAndConfigureMockValidator(true);

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            var playTurnResult =  wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            Assert.IsInstanceOf(typeof(GuessResult), playTurnResult);
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_NullReturned()
        {
            // Arrange
            var userGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);

            var dontCare = new ValidatorResult();

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            var playTurnResult = wordleGame.PlayTurn(userGuess, out dontCare);

            // Assert
            Assert.IsNull(playTurnResult);
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesGuess_ValidatorOutParamIsValid()
        {
            // Arrange
            var validUserGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            var validatorResult = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(validUserGuess, out validatorResult);

            // Assert
            Assert.IsTrue(validatorResult.IsValidGuess());
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesGuess_ValidatorOutParamIsNotValid()
        {
            // Arrange
            var userGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            var validatorResult = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(userGuess, out validatorResult);

            // Assert
            Assert.IsFalse(validatorResult.IsValidGuess());
        }

        [Test] // Maybe Remove - Granularity test for validator object
        public static void PlayTurn_ValidatorInvalidatesFor1Reason_ValidatorOutputHasOnly1FalseField()
        {
            // Arrange
            var guessNot5Letters = "TooLong";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(guessNot5Letters))
                .Return(new ValidatorResult(is5Letters:false,
                                            isAllChars: true, isInDictionary:true));

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            var validatorResult = new ValidatorResult();

            // Act
            wordleGame.PlayTurn(guessNot5Letters, out validatorResult);

            // Assert
            Assert.IsFalse(validatorResult.Is5Letters); // Make ValidatorResult class implement IEnumerable -> use Count(..)?
            Assert.IsTrue(validatorResult.IsAllChars && validatorResult.IsInDictionary);
        }
        
        [Test]
        
        public static void PlayTurn_CorrectGuess_StatusWon()
        {
            // Arrange
            var answer = "bingo";
            var correctGuess = answer; 

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            var valResultdontCare = new ValidatorResult();
            
            // Act 
            wordleGame.PlayTurn(correctGuess, out valResultdontCare);

            // Assert
            Assert.AreEqual(WordleGame.State.Won, wordleGame.Status());
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(WordleGame.MaxNumOfTurns - 1)]
        public static void PlayTurn_PlayedUpToMaxMinus1TurnsWithIncorrectGuess_StatusIsAlive(int numOfTurns)
        {
            // Arrange
            var incorrectGuess = "guess";
            var answer = "bingo";

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            var valResultdontCare = new ValidatorResult();

            // Act 
            for (int currTurn = 0; currTurn < numOfTurns; ++currTurn)
            {
                wordleGame.PlayTurn(incorrectGuess, out valResultdontCare);
            }

            // Assert
            Assert.AreEqual(WordleGame.State.IsAlive, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_MaxTurnsPlayedWithIncorrectGuess_StatusLost()
        {
            // Arrange
            var answer = "bingo";
            var incorrectGuess = "wrong";

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            var valResultdontCare = new ValidatorResult();

            // Act 
            for (int turnNum = 0; turnNum < WordleGame.MaxNumOfTurns; ++turnNum)
            {
                wordleGame.PlayTurn(incorrectGuess, out valResultdontCare);
            }

            // Assert
            Assert.AreEqual(WordleGame.State.Lost, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_CorrectGuess_ReturnedGuessResultObjectIsCorrectGuess()
        {
            // Arrange
            var answer = "bingo";
            var correctGuess = "bingo";

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            var dontCare = new ValidatorResult();

            // Act 
            var guessResult = wordleGame.PlayTurn(correctGuess, out dontCare);

            // Assert
            Assert.IsTrue(guessResult.IsCorrectGuess());
        }

        [Test]
        public static void PlayTurn_CorrectGuessOnLastTurn_StatusWon()
        {
            // Arrange
            var answer = "bingo";
            var incorrectGuess = "wrong";
            var correctGuessForFinalTurn = "bingo";

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            var dontCare = new ValidatorResult();

            // Act 
            for (int turnNum = 0; turnNum < WordleGame.MaxNumOfTurns - 1; ++turnNum)
            {
                wordleGame.PlayTurn(incorrectGuess, out dontCare);
            }

            wordleGame.PlayTurn(correctGuessForFinalTurn, out dontCare);

            // Assert
            Assert.AreEqual(WordleGame.State.Won, wordleGame.Status());
        }

    }
}
