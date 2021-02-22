namespace ExpressionTreeThing
{
    public class Token
    {
        public enum TokenType
        {
            Variable,
            Constant,
            Operator,
            LeftParen,
            RightParen
        }

        public TokenType Type;
        public string Value;

        public Token(TokenType type, string value)
        {
            Value = value;
            Type = type;
        }
    }
}
