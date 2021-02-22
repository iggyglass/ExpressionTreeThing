using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTreeThing
{
    class ExpressionEvaluator
    {
        public static Func<double[], double> GetExpression(List<Token> tokens)
        {
            ParameterExpression parameterExpr = Expression.Parameter(typeof(double[]));
            Stack<Expression> expressions = new Stack<Expression>();
            Dictionary<string, int> variableToIndex = new Dictionary<string, int>();
            int current = 0;

            foreach (var token in tokens)
            {
                if (token.Type == Token.TokenType.Variable && !variableToIndex.ContainsKey(token.Value))
                {
                    variableToIndex.Add(token.Value, current);
                    current++;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == Token.TokenType.Constant)
                {
                    expressions.Push(Expression.Constant(double.Parse(tokens[i].Value)));
                }
                else if (tokens[i].Type == Token.TokenType.Variable)
                {
                    var expr = Expression.ArrayIndex(parameterExpr, Expression.Constant(variableToIndex[tokens[i].Value]));
                    expressions.Push(expr);
                }
                else // Token is an operator
                {
                    Expression left = expressions.Pop();
                    Expression right = expressions.Pop();
                    Expression newExpression;

                    switch (tokens[i].Value) // TODO: fix this bc string switching is bad (iirc)
                    {
                        case "+":
                            newExpression = Expression.Add(left, right);
                            break;
                        case "-":
                            newExpression = Expression.Subtract(left, right);
                            break;
                        case "*":
                            newExpression = Expression.Multiply(left, right);
                            break;
                        case "/":
                            newExpression = Expression.Divide(left, right);
                            break;
                        default:
                            throw new Exception();
                    }

                    expressions.Push(newExpression);
                }
            }

            LambdaExpression lambda = Expression.Lambda<Func<double[], double>>(expressions.Pop(), parameterExpr);
            return (Func<double[], double>)lambda.Compile();
        }
    }
}
