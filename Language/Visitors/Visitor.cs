namespace Language.Visitors
{
    public abstract class Visitor<TReturn>
    {
        public TReturn Visit(AST.Expression expr)
        {
            return expr.Accept(this);
        }

        internal abstract TReturn VisitBinary(AST.Expression.BinaryExpression binary);
        internal abstract TReturn VisitUnary(AST.Expression.UnaryExpression unary);
        internal abstract TReturn VisitGrouping(AST.Expression.GroupingExpression grouping);
        internal abstract TReturn VisitLiteral<TValue>(AST.Expression.LiteralExpression<TValue> literal);
    }
}
