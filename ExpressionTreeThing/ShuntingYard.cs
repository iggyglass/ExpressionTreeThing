using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionTreeThing
{
    public class ShuntingYard
    {

        private static Dictionary<string, int> operatorPrecedence = new Dictionary<string, int>
        {
            ["+"] = 2,
            ["-"] = 2,
            ["*"] = 3,
            ["/"] = 3
        };

        public static Queue<Token> ConvertToTokens(string input) // TODO: This
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {

            }
        }

        public static Queue<Token> ReversePolishNotation(Queue<Token> tokens)
        {
            Queue<Token> output = new Queue<Token>();
            Stack<Token> operatorStack = new Stack<Token>();

            while (tokens.Count > 0)
            {
                Token token = tokens.Dequeue();

                if (token.Type == Token.TokenType.Variable || token.Type == Token.TokenType.Constant)
                {
                    output.Enqueue(token);
                }
                else if (token.Type == Token.TokenType.Operator)
                {
                    int currentPrecedence = operatorPrecedence[token.Value];

                    while (operatorStack.Count > 0 && operatorStack.Peek().Type != Token.TokenType.LeftParen && operatorPrecedence[operatorStack.Peek().Value] >= currentPrecedence)
                    {
                        output.Enqueue(operatorStack.Pop());
                    }
                }
                else if (token.Type == Token.TokenType.LeftParen)
                {
                    operatorStack.Push(token);
                }
                else if (token.Type == Token.TokenType.RightParen)
                {
                    while (operatorStack.Peek().Type != Token.TokenType.LeftParen)
                    {
                        output.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Pop();
                }
            }

            while (operatorStack.Count > 0)
            {
                output.Enqueue(operatorStack.Pop());
            }

            return output;
        }
    }
}
