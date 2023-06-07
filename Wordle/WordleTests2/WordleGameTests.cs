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
            wordleGame.PlayTurn(userGuess);

            // assert
            Assert.AreEqual(initialTurnsRemaining, wordleGame.TurnsRemaining());
        }



        [Test]
        public static void PlayTurn_PlayTurnInvoked_ValidatorIsAlwaysCalled()
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate("")).IgnoreArguments().Return(new ValidatorResult());
            
            var wordleGame = new WordleGame(new GuessAnalyzer("dontCare"), mockValidator);
            
            // Act
            wordleGame.PlayTurn("");

            // Assert
            mockValidator.AssertWasCalled(v => v.Validate(""));
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_GuessAnalyzerIsNotCalled()
        {
            // Arrange 
            var invalidUserGuess = "NotValid";
            var mockInvalidatingValidator = CreateAndConfigureMockValidator(false);
            var guessAnalyzer = MockRepository.GenerateStub<IGuessAnalyzer>();
            var wordleGame = new WordleGame(guessAnalyzer, mockInvalidatingValidator);

            // Act
            wordleGame.PlayTurn(invalidUserGuess);

            // Assert
            guessAnalyzer.AssertWasNotCalled(v => v.Analyze(invalidUserGuess));
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_GuessAnalyzerIsCalled()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidatingValidator = CreateAndConfigureMockValidator(true);
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();
            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidatingValidator);

            // Act
            wordleGame.PlayTurn(userGuess);

            // Assert
            mockGuessAnalyzer.AssertWasCalled(v => v.Analyze(userGuess));
        }


        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_RemainingTurnsDecrementedByOne()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();
            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            
            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            // Act
            wordleGame.PlayTurn(userGuess);

            // Assert
            Assert.AreEqual(initialTurnsRemaining - 1, wordleGame.TurnsRemaining());
        }

        
        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_GuessResultIsNotValid()
        {
            // Arrange
            var userGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);

            // Act
            var playTurnResult = wordleGame.PlayTurn(userGuess);

            // Assert
            Assert.IsFalse(playTurnResult.IsValid());
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesGuess_GuessResultIsValid()
        {
            // Arrange
            var validUserGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsCorrect();

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);

            // Act
            var playTurnGuessResult = wordleGame.PlayTurn(validUserGuess);

            // Assert
            Assert.IsTrue(playTurnGuessResult.IsValid());
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
            
            wordleGame.PlayTurn(guessNot5Letters);

            // Assert
            // TODO Assert validatorResult.ErrorCount is 1
            
        }
        
        [Test]
        
        public static void PlayTurn_CorrectGuess_StatusWon()
        {
            // Arrange
            var answer = "bingo";
            var correctGuess = answer;
            var mockValidator = CreateAndConfigureMockValidator(true);
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsCorrect();
            
            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
            
            // Act 
            wordleGame.PlayTurn(correctGuess);

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
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();
            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);

            // Act 
            for (int currTurn = 0; currTurn < numOfTurns; ++currTurn)
            {
                
                wordleGame.PlayTurn(incorrectGuess);
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
            var guessResult = wordleGame.PlayTurn(correctGuess);

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
            var mockGuessAnalyzer = CreateMockGuessAnalyzerReturnsIncorrect();

            var wordleGame = new WordleGame(mockGuessAnalyzer, mockValidator);
 
            // Act 
            for (int turnNum = 0; turnNum < WordleGame.MaxNumOfTurns; ++turnNum)
            {
                
                wordleGame.PlayTurn(incorrectGuess);
            }

            // Assert
            Assert.AreEqual(WordleGame.State.Lost, wordleGame.Status());
        }

        [Test]
        // TODO mockAnalyzer that returns true for correctAnswer
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
                
                wordleGame.PlayTurn(incorrectGuess);
            }

            wordleGame.PlayTurn(correctGuessForFinalTurn);

            // Assert
            Assert.AreEqual(WordleGame.State.Won, wordleGame.Status());
        }

    }
}
