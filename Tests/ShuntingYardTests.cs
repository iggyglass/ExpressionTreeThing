using System;
using Xunit;
using ExpressionTreeThing;
using System.Collections.Generic;
using System.Collections;

namespace Tests
{
    public class ShuntingYardTests
    {
        [Theory]
        [ClassData(typeof(StringToTokenConversionTestData))]
        public void StringToTokenConversionTest(string input, Token[] expected)
        {
            Queue<Token> output = ShuntingYard.ConvertToTokens(input);

            foreach (var current in expected)
            {
                Token token = output.Dequeue();

                Assert.Equal(current.Type, token.Type);
                Assert.Equal(current.Value, token.Value);
            }
        }

        [Theory]
        [ClassData(typeof(TokensToReversePolishNotationTestData))]
        public void TokensToReversePolishNotationTest(Token[] input, Token[] expected)
        {
            Queue<Token> output = ShuntingYard.ReversePolishNotation(new Queue<Token>(input));

            foreach (var current in expected)
            {
                Token token = output.Dequeue();

                Assert.Equal(current.Type, token.Type);
                Assert.Equal(current.Value, token.Value);
            }
        }
    }
}
