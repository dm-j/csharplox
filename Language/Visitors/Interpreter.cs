using Language.AST;

namespace Language.Visitors
{
    public class Interpreter : Visitor<object>
    {
        private readonly Action<string> Error;
        private readonly Action<string> Success;

        public Interpreter(Action<string> error, Action<string> success)
        {
            Error = error;
            Success = success;
        }

        internal void Interpret(Expression expression)
        {
            try
            {
                object value = Visit(expression);
                Success(value.ToString() ?? string.Empty);
            }
            catch (RuntimeError error)
            {
                Error(error.Message);
            }
        }

        internal override object VisitBinary(Expression.BinaryExpression binary)
        {
            object left = Visit(binary.Left);
            object right = Visit(binary.Right);

            if (left.GetType() != right.GetType())
            {
                return new RuntimeError(binary.Operator, $"Mismatched operand types: {left.GetType().Name}<->{right.GetType().Name}");
            }

            if (left is decimal leftDecimal && right is decimal rightDecimal)
            {
                switch (binary.Operator)
                {
                    case Minus:
                        return leftDecimal - rightDecimal;
                    case Star:
                        return leftDecimal * rightDecimal;
                    case Slash when rightDecimal == 0:
                        throw new RuntimeError(binary.Operator, "Cannot divide by zero");
                    case Slash:
                        return leftDecimal / rightDecimal;
                    case Plus:
                        return leftDecimal + rightDecimal;
                    case Less:
                        return leftDecimal < rightDecimal;
                    case LessEqual:
                        return leftDecimal <= rightDecimal;
                    case Greater:
                        return leftDecimal > rightDecimal;
                    case GreaterEqual:
                        return leftDecimal >= rightDecimal;
                    case BangEqual:
                        return leftDecimal != rightDecimal;
                    case EqualEqual:
                        return leftDecimal == rightDecimal;
                    default:
                        Error($"Unsupported Binary Operator: {binary.Operator}");
                        return new();
                }
            }

            if (left is string leftString && right is string rightString)
            {
                return binary.Operator switch
                {
                    Plus => leftString + rightString,
                    _ => throw new RuntimeError(binary.Operator, $"{binary.Operator.Lexeme} is not supported for string"),
                };
            }

            throw new RuntimeError(binary.Operator, $"{binary.Operator.Lexeme} is not supported for {left.GetType().Name}");
        }

        internal override object VisitGrouping(Expression.GroupingExpression grouping)
        {
            return Visit(grouping);
        }

        internal override object VisitLiteral<TValue>(Expression.LiteralExpression<TValue> literal)
        {
            return literal.Value;
        }

        internal override object VisitUnary(Expression.UnaryExpression unary)
        {
            object right = Visit(unary.Value);

            return unary.Operator switch
            {
                Minus when right is decimal decimalRight => -decimalRight,
                Minus => throw new RuntimeError(unary.Operator, $"{unary.Operator} is not supported for {right.GetType().Name}"),
                Bang => !IsTruthy(right),
                _ => throw new RuntimeError(unary.Operator, $"Unrecognized Unary Operator: {unary.Operator}"),
            };
        }

        private static bool IsTruthy(object obj)
        {
            return obj switch
            {
                bool b => b,
                null => false,
                _ => true,
            };
        }
    }
}
