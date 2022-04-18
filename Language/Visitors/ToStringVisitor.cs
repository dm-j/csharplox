namespace Language.Visitors
{
    public class ToStringVisitor : Visitor<string>
    {
        internal override string VisitBinary(AST.Expression.BinaryExpression binary)
        {
            return $"{Visit(binary.Left)} {binary.Operator.Lexeme} {Visit(binary.Right)}";
        }

        internal override string VisitGrouping(AST.Expression.GroupingExpression grouping)
        {
            return $"({Visit(grouping.Value)})";
        }

        internal override string VisitLiteral<TValue>(AST.Expression.LiteralExpression<TValue> literal)
        {
            return literal.Value.Lexeme;
        }

        internal override string VisitUnary(AST.Expression.UnaryExpression unary)
        {
            return $"{unary.Operator.Lexeme}{Visit(unary.Value)}";
        }
    }
}
