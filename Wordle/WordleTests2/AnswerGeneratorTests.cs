using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Wordle;

namespace WordleTests
{
    class AnswerGeneratorTests
    {
        [Test]
        public static void GenerateAnswer_EmptyStringParam_Null()
        {
            // arrange
            string[] emptyStringArray = Array.Empty<string>();
            var answerGenerator = new AnswerGenerator(emptyStringArray);

            // act
            var answerResult = answerGenerator.GenerateAnswer();

            // assert
            Assert.IsNull(answerResult);
        }
    }
}
