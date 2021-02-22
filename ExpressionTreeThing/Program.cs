using System;
using System.Collections.Generic;

namespace ExpressionTreeThing
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Give me expression: ");
                string rawExpr = Console.ReadLine();

                Console.WriteLine("Give comma separated values: ");
                string rawValues = Console.ReadLine();

                Queue<Token> tokens = ShuntingYard.ConvertToTokens(rawExpr);
                Queue<Token> rpn = ShuntingYard.ReversePolishNotation(tokens);
                Func<double[], double> method = ExpressionEvaluator.GetExpression(new List<Token>(rpn));

                double[] values = convertToDoubleArray(rawValues);

                Console.WriteLine(method(values));
            }
        }

        static double[] convertToDoubleArray(string value)
        {
            string[] values = value.Split(',');
            double[] data = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                data[i] = double.Parse(values[i]);
            }

            return data;
        }
    }
}
