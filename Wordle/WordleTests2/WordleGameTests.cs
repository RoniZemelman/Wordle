﻿using System;
using NUnit.Framework;
using Rhino.Mocks;
using Wordle;
using static Wordle.GuessValidator;
using static WordleTests.WordleGameTestsUtils;


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
            var wordleGame = new WordleGame("TodoRemove", mockValidator);
            
            // Assert
            Assert.AreEqual(WordleGame.MaxNumOfTurns, wordleGame.TurnsRemaining());
        }

        [Test]
        public static void Constructor_WordleGameCreated_StatusIsAlive()
        {
            // Arrange
            var mockValidator = CreateAndConfigureMockValidator(false);

            // Act
            var wordleGame = new WordleGame("TodoRemove", mockValidator);

            // Assert
            Assert.AreEqual(WordleGame.State.IsRunning, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_RemainingTurnsUnchanged()
        {
            // Arrange
            string userGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);
            
            var wordleGame = new WordleGame("TodoRemove", mockValidator);
            int initialTurnsRemaining = wordleGame.TurnsRemaining();

            // Act
            wordleGame.PlayTurn(userGuess);

            // assert
            Assert.AreEqual(initialTurnsRemaining, wordleGame.TurnsRemaining());
        }

        [Test]
        [TestCase("valid")]
        [TestCase("Invalid")]
        public static void PlayTurn_PlayTurnInvokedWithValidAndInvalidGuesses_ValidatorIsAlwaysCalled(string userGuess)
        {
            // Arrange 
            var mockValidator = MockRepository.GenerateStub<IWordValidator>();
            mockValidator.Stub(v => v.Validate(userGuess)).IgnoreArguments().Return(new ValidatorResult());
            var wordleGame = new WordleGame("TodoRemove", mockValidator);
            
            // Act
            wordleGame.PlayTurn(userGuess);

            // Assert
            mockValidator.AssertWasCalled(v => v.Validate(userGuess));
        }

        [Test]
        public static void PlayTurn_ValidatorInvalidatesUserGuess_GuessResultIsNull()
        {
            // Arrange 
            var invalidatedUserGuess = "NotValid";
            var mockInvalidatingValidator = CreateAndConfigureMockValidator(false);
            var wordleGame = new WordleGame("TodoRemove", mockInvalidatingValidator);

            // Act
            var guessResult = wordleGame.PlayTurn(invalidatedUserGuess);

            // Assert
            Assert.IsTrue(guessResult.IsNull());
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_GuessResultIsNotNull()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidatingValidator = CreateAndConfigureMockValidator(true);
            var wordleGame = new WordleGame("TodoRemove", mockValidatingValidator);

            // Act
            var guessResult = wordleGame.PlayTurn(userGuess);

            // Assert
            Assert.IsFalse(guessResult.IsNull());
        }


        [Test]
        public static void PlayTurn_ValidatorValidatesUserGuess_RemainingTurnsDecrementedByOne()
        {
            // Arrange 
            var userGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);
            var wordleGame = new WordleGame("TodoRemove", mockValidator);
            
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
            var invalidUserGuess = "InvalidGuess";
            var mockValidator = CreateAndConfigureMockValidator(false);
            var wordleGame = new WordleGame("TodoRemove", mockValidator);

            // Act
            var playTurnResult = wordleGame.PlayTurn(invalidUserGuess);

            // Assert
            Assert.IsFalse(playTurnResult.IsValid());
        }

        [Test]
        public static void PlayTurn_ValidatorValidatesGuess_GuessResultIsValid()
        {
            // Arrange
            var validUserGuess = "valid";
            var mockValidator = CreateAndConfigureMockValidator(true);
            var wordleGame = new WordleGame("TodoRemove", mockValidator);

            // Act
            var playTurnGuessResult = wordleGame.PlayTurn(validUserGuess);

            // Assert
            Assert.IsTrue(playTurnGuessResult.IsValid());
        }

        [Test] // Granularity test for validator object
        public static void PlayTurn_ValidatorInvalidatesFor1Reason_ValidatorOutputHasOnly1FalseField()
        {
            // Arrange
            var guessNot5Letters = "TooLong";
            var mockEnglishDictionary = MockRepository.GenerateStub<IEnglishDictionary>();
            mockEnglishDictionary
                .Stub(d => d.IsInDictionary(guessNot5Letters))
                .Return(true);

            var wordleGame = new WordleGame("TodoRemove",
                                        new GuessValidator(mockEnglishDictionary));

            // Act
            var guessResult = wordleGame.PlayTurn(guessNot5Letters);

            // Assert
            Assert.AreEqual(1, guessResult.ValidationResult.ErrorCount());
         }
        
        [Test]
        
        public static void PlayTurn_CorrectGuess_StatusWon()
        {
            // Arrange
            var answer = "bingo"; // TODO remove 
            var correctGuess = answer; 
            var mockValidator = CreateAndConfigureMockValidator(true);
            var wordleGame = new WordleGame(answer, mockValidator);
            
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
            var mockValidator = CreateAndConfigureMockValidator(true);
            var wordleGame = new WordleGame("TodoRemove", mockValidator);

            // Act 
            for (int currentTurn = 0; currentTurn < numOfTurns; ++currentTurn)
            {
                wordleGame.PlayTurn("false");
            }

            // Assert
            Assert.AreEqual(WordleGame.State.IsRunning, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_CorrectGuess_ReturnedGuessResultObjectIsCorrectGuess()
        {
            // Arrange
            var answer = "bingo";
            var correctGuess = answer;
            var mockValidator = CreateAndConfigureMockValidator(true);
            var wordleGame = new WordleGame(answer, mockValidator);

            // Act 
            var guessResult = wordleGame.PlayTurn(correctGuess);

            // Assert
            Assert.IsTrue(guessResult.IsCorrectGuess());
        }

        [Test]
        public static void PlayTurn_MaxTurnsPlayedWithIncorrectGuess_StatusLost()
        {
            // Arrange
            var incorrectGuess = "false";
            var mockValidator = CreateAndConfigureMockValidator(true);

            var wordleGame = new WordleGame("TodoRemove", mockValidator);
 
            // Act 
            for (int turnNum = 0; turnNum < WordleGame.MaxNumOfTurns; ++turnNum)
            {
                wordleGame.PlayTurn(incorrectGuess);
            }

            // Assert
            Assert.AreEqual(WordleGame.State.Lost, wordleGame.Status());
        }

        [Test]
        public static void PlayTurn_UserAccessesInvalidatedGuessResult_InvalidOperationExceptionThrown()
        {
            // Arrange
            var invalidGuess = "guessWillBeInvalidated";
            var mockValidator = CreateAndConfigureMockValidator(false);

            var wordleGame = new WordleGame("TodoRemove", mockValidator);

            // Act 
            var guessResult = wordleGame.PlayTurn(invalidGuess);

            // Assert
            Assert.Throws<InvalidOperationException>(() => guessResult.GetNumExactMatches());
            Assert.Throws<InvalidOperationException>(() => guessResult.GetNumPartialMatches());
            Assert.Throws<InvalidOperationException>(() => guessResult.At(0));
        }

    }
}
