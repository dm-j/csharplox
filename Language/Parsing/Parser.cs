using Language.AST;
using static Language.AST.Expression;

namespace Language.Parsing
{
    public class Parser
    {
        private readonly TokenStream Tokens;
        private readonly Action<string> Error;

        public Parser(IEnumerable<Token> tokens, Action<string> error)
        {
            Tokens = new(tokens);
            Error = error;
        }

        public Expression? Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParseError ex)
            {
                Error(ex.Message);
                return null;
            }
        }

        private Expression Expression()
        {
            return Equality();
        }

        private Expression Equality()
        {
            Expression expression = Comparison();

            while (Match<Equality>())
            {
                Binary op = (Binary)Previous();

                Expression right = Comparison();

                expression = new BinaryExpression(expression, op, right);
            }

            return expression;
        }

        private Expression Comparison()
        {
            Expression expression = Term();

            while (Match<Comparison>())
            {
                Binary op = (Binary)Previous();

                Expression right = Term();

                expression = new BinaryExpression(expression, op, right);
            }

            return expression;
        }

        private Expression Term()
        {
            Expression expression = Factor();

            while (Match<Term>())
            {
                Binary op = (Binary)Previous();

                Expression right = Factor();

                expression = new BinaryExpression(expression, op, right);
            }

            return expression;
        }

        private Expression Factor()
        {
            Expression expression = Unary();

            while (Match<Factor>())
            {
                Binary op = (Binary)Previous();

                Expression right = Unary();

                expression = new BinaryExpression(expression, op, right);
            }

            return expression;
        }

        private Expression Unary()
        {
            if (Match<Unary>())
            {
                Unary op = (Unary)Previous();

                Expression right = Unary();

                return new UnaryExpression(op, right);
            }

            return Primary();
        }

        private Expression Primary()
        {
            var token = Advance();
            switch (token)
            {
                case Literal<bool> b:
                    return new LiteralExpression<bool>(b);
                case Literal<string> s:
                    return new LiteralExpression<string>(s);
                case Literal<decimal> d:
                    return new LiteralExpression<decimal>(d);
                case Literal<object?>:
                    return new LiteralExpression<object?>(null!);
                case ParenLeft:
                    Expression expression = Expression();
                    var _ = Consume<ParenRight>("Expected ')' after '('");
                    return new GroupingExpression(expression);
                default:
                    throw new ParseError($"I don't support token: {token}");
            }
        }

        public Token Advance()
        {
            return Tokens.Advance();
        }

        public bool Check<TToken>()
        {
            return Tokens.Check<TToken>();
        }

        public bool IsAtEnd()
        {
            return Tokens.IsAtEnd();
        }

        public bool Match<TToken>()
        {
            return Tokens.Match<TToken>();
        }

        public Token Peek()
        {
            return Tokens.Peek();
        }

        public Token Previous()
        {
            return Tokens.Previous();
        }

        public Token Consume<TToken>(string message)
        {
            return Tokens.Consume<TToken>(message);
        }

        public void Synchronize()
        {
            Advance();

            while (!IsAtEnd())
            {
                if (Previous() is Semicolon)
                {
                    return;
                }

                switch (Peek())
                {
                    case Class:
                    case Fun:
                    case Var:
                    case For:
                    case If:
                    case While:
                    case Print:
                    case Return:
                        return;
                    default:
                        break;
                }

                Advance();
            }
        }
    }
}
