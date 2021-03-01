using System;
using Xunit;
using ExpressionTreeThing;
using System.Collections.Generic;
using System.Collections;

namespace Tests
{
    internal class TokensToReversePolishNotationTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly List<object[]> data = new List<object[]>()
        {
            // Tests simple expression without variables
            new object[] 
            { 
                // Input
                new Token[] 
                {
                    new Token(Token.TokenType.Constant, "3"),
                    new Token(Token.TokenType.Operator, "+"),
                    new Token(Token.TokenType.Constant, "4"),
                    new Token(Token.TokenType.Operator, "*"),
                    new Token(Token.TokenType.LeftParen, "("),
                    new Token(Token.TokenType.Constant, "2"),
                    new Token(Token.TokenType.Operator, "-"),
                    new Token(Token.TokenType.Constant, "1"),
                    new Token(Token.TokenType.RightParen, ")")
                },

                // Output
                new Token[]
                {
                    new Token(Token.TokenType.Constant, "3"),
                    new Token(Token.TokenType.Constant, "4"),
                    new Token(Token.TokenType.Constant, "2"),
                    new Token(Token.TokenType.Constant, "1"),
                    new Token(Token.TokenType.Operator, "-"),
                    new Token(Token.TokenType.Operator, "*"),
                    new Token(Token.TokenType.Operator, "+")
                }
            },

            // Tests a simple expression with variables
            new object[]
            {
                // Input
                new Token[]
                {
                    new Token(Token.TokenType.Variable, "a"),
                    new Token(Token.TokenType.Operator, "+"),
                    new Token(Token.TokenType.Variable, "b"),
                    new Token(Token.TokenType.Operator, "*"),
                    new Token(Token.TokenType.LeftParen, "("),
                    new Token(Token.TokenType.Constant, "2"),
                    new Token(Token.TokenType.Operator, "-"),
                    new Token(Token.TokenType.Variable, "xyz"),
                    new Token(Token.TokenType.RightParen, ")")
                },

                // Output
                new Token[]
                {
                    new Token(Token.TokenType.Variable, "a"),
                    new Token(Token.TokenType.Variable, "b"),
                    new Token(Token.TokenType.Constant, "2"),
                    new Token(Token.TokenType.Variable, "xyz"),
                    new Token(Token.TokenType.Operator, "-"),
                    new Token(Token.TokenType.Operator, "*"),
                    new Token(Token.TokenType.Operator, "+")
                }
            }
        };
    }
}
