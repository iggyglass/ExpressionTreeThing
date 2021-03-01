using System;
using Xunit;
using ExpressionTreeThing;
using System.Collections.Generic;
using System.Collections;

namespace Tests
{
    internal class StringToTokenConversionTestData : IEnumerable<object[]>
    {
        // TODO: test stuff with decimals
        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly List<object[]> data = new List<object[]>()
        {
            // Tests weird spacing stuff
            new object[] { "2+2            * 32 / 3 +abc", new Token[]
            {
                new Token(Token.TokenType.Constant, "2"),
                new Token(Token.TokenType.Operator, "+"),
                new Token(Token.TokenType.Constant, "2"),
                new Token(Token.TokenType.Operator, "*"),
                new Token(Token.TokenType.Constant, "32"),
                new Token(Token.TokenType.Operator, "/"),
                new Token(Token.TokenType.Constant, "3"),
                new Token(Token.TokenType.Operator, "+"),
                new Token(Token.TokenType.Variable, "abc")
            } },

            // Tests doing stuff with parenthesis
            new object[] { "5*(7+3)", new Token[]
            {
                new Token(Token.TokenType.Constant, "5"),
                new Token(Token.TokenType.Operator, "*"),
                new Token(Token.TokenType.LeftParen, "("),
                new Token(Token.TokenType.Constant, "7"),
                new Token(Token.TokenType.Operator, "+"),
                new Token(Token.TokenType.Constant, "3"),
                new Token(Token.TokenType.RightParen, ")")
            } },

            // Tests a fairly normal usecase
            new object[] { "2*a+(bcd/z)*1234", new Token[]
            {
                new Token(Token.TokenType.Constant, "2"),
                new Token(Token.TokenType.Operator, "*"),
                new Token(Token.TokenType.Variable, "a"),
                new Token(Token.TokenType.Operator, "+"),
                new Token(Token.TokenType.LeftParen, "("),
                new Token(Token.TokenType.Variable, "bcd"),
                new Token(Token.TokenType.Operator, "/"),
                new Token(Token.TokenType.Variable, "z"),
                new Token(Token.TokenType.RightParen, ")"),
                new Token(Token.TokenType.Operator, "*"),
                new Token(Token.TokenType.Constant, "1234")
            } }
        };
    }
}
