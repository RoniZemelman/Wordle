﻿using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static WordleTests.Utils;


namespace WordleTests
{
    class WordleGameTests
    {         
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

            // Act
            GuessResult guessResult = new GuessResult();
            wordleGame.PlayTurn(userGuess, out guessResult);

            // assert
            Assert.AreEqual(initialTurnsRemaining, wordleGame.TurnsRemaining());
        }



        [Test]
        public static void PlayTurn_PlayTurnInvoked_ValidatorIsAlwaysCalled()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            var dontCare = new ValidatorResult();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments().Return(dontCare);
            
            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);
            
            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn("", out guessResult);

            // Assert
            mockValidator.AssertWasCalled(v => v.Validate(""));
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_GuessAnalyzerIsNotCalled()
        {
            // Arrange 
            var invalidUserGuess = "NotValid";
            var mockInvalidatingValidator = CreateAndConfigureMockValidator(false);
            var mockGuessAnalzer = MockRepository.GenerateStub<IGuessAnalyzer>();
            var wordleGame = new WordleGame(mockGuessAnalzer, mockInvalidatingValidator);

            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn(invalidUserGuess, out guessResult);

            // Assert
            mockGuessAnalzer.AssertWasNotCalled(v => v.Analyze(invalidUserGuess));
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_GuessAnalyzerIsCalled()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidatingValidator = CreateAndConfigureMockValidator(true);

            var mockGuessAnalzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalzer, mockValidatingValidator);

            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn(userGuess, out guessResult);

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
            
            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn(userGuess, out guessResult);

            // Assert
            Assert.AreEqual(initialTurnsRemaining - 1, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_GuessResultObjectReturned()
        {
            // Arrange
            var userGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            GuessResult guessResult;
            var playTurnResult =  wordleGame.PlayTurn(userGuess, out guessResult);

            // Assert
            Assert.IsInstanceOf(typeof(GuessResult), playTurnResult);
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_NullReturned()
        {
            // Arrange
            var userGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            GuessResult guessResult;
            var playTurnResult = wordleGame.PlayTurn(userGuess, out guessResult);

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

         

            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn(validUserGuess, out guessResult);


            // Assert
            // TODO Assert GuessResult is a ValidGuess
            // Assert.IsTrue(validatorResult.IsValidGuess());
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesGuess_ValidatorOutParamIsNotValid()
        {
            // Arrange
            var userGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn(userGuess, out guessResult);

            // Assert
            //  TODO Assert GuessResult is a ValidGuess
            //Assert.IsFalse(validatorResult.IsValidGuess());
        }

        [Test] // Remove - Granularity test for validator object
        public static void PlayTurn_ValidatorInvalidatesFor1Reason_ValidatorOutputHasOnly1FalseField()
        {
            // Arrange
            var guessNot5Letters = "TooLong";
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(d => d.Validate(guessNot5Letters))
                .Return(new ValidatorResult(is5Letters:false,
                                            isAllChars: true, isInDictionary:true));

            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);

            // Act
            GuessResult guessResult;
            wordleGame.PlayTurn(guessNot5Letters, out guessResult);

            // Assert
            // TODO Assert validatorResult.ErrorCount is 1
            //Assert.IsFalse(validatorResult.Is5Letters); // Make ValidatorResult class implement IEnumerable -> use Count(..)?
            //Assert.IsTrue(validatorResult.IsAllChars && validatorResult.IsInDictionary);
        }
        
        [Test]
        
        public static void PlayTurn_CorrectGuess_StatusWon()
        {
            // Arrange
            var answer = "bingo";
            var correctGuess = answer; 

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
            
            // Act 
            GuessResult guessResult;
            wordleGame.PlayTurn(correctGuess, out guessResult);

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

            // Act 
            for (int currTurn = 0; currTurn < numOfTurns; ++currTurn)
            {
                GuessResult guessResult;
                wordleGame.PlayTurn(incorrectGuess, out guessResult);
            }

            // Assert
            Assert.AreEqual(WordleGame.State.IsAlive, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_CorrectGuess_ReturnedGuessResultObjectIsCorrectGuess()
        {
            // Arrange
            var answer = "bingo";
            var correctGuess = "bingo";

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);

            // Act 
            GuessResult guessResult1;
            var guessResult = wordleGame.PlayTurn(correctGuess, out guessResult1);

            // Assert
            Assert.IsTrue(guessResult.IsCorrectGuess());
        }

        [Test]
        public static void PlayTurn_MaxTurnsPlayedWithIncorrectGuess_StatusLost()
        {
            // Arrange
            var answer = "bingo";
            var incorrectGuess = "wrong";

            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame(new GuessAnalyzer(answer), mockValidator);
 
            // Act 
            for (int turnNum = 0; turnNum < WordleGame.MaxNumOfTurns; ++turnNum)
            {
                GuessResult guessResult;
                wordleGame.PlayTurn(incorrectGuess, out guessResult);
            }

            // Assert
            Assert.AreEqual(WordleGame.State.Lost, wordleGame.Status());
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

            // Act 
            for (int turnNum = 0; turnNum < WordleGame.MaxNumOfTurns - 1; ++turnNum)
            {
                GuessResult guessResult;
                wordleGame.PlayTurn(incorrectGuess, out guessResult);
            }

            GuessResult guessResult1;
            wordleGame.PlayTurn(correctGuessForFinalTurn, out guessResult1);

            // Assert
            Assert.AreEqual(WordleGame.State.Won, wordleGame.Status());
        }

    }
}