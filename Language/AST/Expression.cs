using Language.Visitors;

namespace Language.AST
{
    public abstract record Expression 
    {
        public abstract TReturn Accept<TReturn>(Visitor<TReturn> visitor);

        public record BinaryExpression(Expression Left, Binary Operator, Expression Right) : Expression
        {
            public override TReturn Accept<TReturn>(Visitor<TReturn> visitor)
            {
                return visitor.VisitBinary(this);
            }
        }

        public record UnaryExpression(Unary Operator, Expression Value) : Expression
        {
            public override TReturn Accept<TReturn>(Visitor<TReturn> visitor)
            {
                return visitor.VisitUnary(this);
            }
        }

        public record GroupingExpression(Expression Value) : Expression
        {
            public override TReturn Accept<TReturn>(Visitor<TReturn> visitor)
            {
                return visitor.VisitGrouping(this);
            }
        }

        public record LiteralExpression<TValue>(Literal<TValue> Value) : Expression
        {
            public override TReturn Accept<TReturn>(Visitor<TReturn> visitor)
            {
                return visitor.VisitLiteral(this);
            }
        }
    }
}
