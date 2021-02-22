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

        public static Queue<Token> ConvertToTokens(string input)
        {
            StringBuilder builder = new StringBuilder();
            Queue<Token> output = new Queue<Token>();
            Token.TokenType prevType = getType(input[0].ToString());

            for (int i = 0; i < input.Length; i++)
            {
                Token.TokenType currentType = getType(input[i].ToString());

                if (currentType != prevType)
                {
                    Token token = new Token(prevType, builder.ToString());

                    output.Enqueue(token);
                    builder.Clear();
                    prevType = currentType;
                    builder.Append(input[i]);
                }
                else
                {
                    builder.Append(input[i]);
                }
            }

            if (builder.Length > 0)
            {
                Token token = new Token(prevType, builder.ToString());
                output.Enqueue(token);
            }

            return output;
        }

        private static Token.TokenType getType(string input)
        {
            if (Regex.IsMatch(input, @"\+|\-|\*|\/")) return Token.TokenType.Operator;
            else if (Regex.IsMatch(input, @"[A-Za-z]")) return Token.TokenType.Variable;
            else if (Regex.IsMatch(input, @"[0-9]|\.")) return Token.TokenType.Constant;
            else if (Regex.IsMatch(input, @"\(")) return Token.TokenType.LeftParen;
            else if (Regex.IsMatch(input, @"\)")) return Token.TokenType.RightParen;
            else throw new Exception("reeee");
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

                    operatorStack.Push(token);
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
