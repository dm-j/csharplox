#pragma warning disable IDE1006 // Naming Styles
namespace Language
{
    public abstract record Token(string Lexeme, int Line)
    {
        public static Token ParenLeft(int line) => new ParenLeft(line);
        public static Token ParenRight(int line) => new ParenRight(line);
        public static Token BraceLeft(int line) => new BraceLeft(line);
        public static Token BraceRight(int line) => new BraceRight(line);
        public static Token Comma(int line) => new Comma(line);
        public static Token Dot(int line) => new Dot(line);
        public static Token Minus(int line) => new Minus(line);
        public static Token Plus(int line) => new Plus(line);
        public static Token Semicolon(int line) => new Semicolon(line);
        public static Token Slash(int line) => new Slash(line);
        public static Token Star(int line) => new Star(line);
        public static Token Bang(int line) => new Bang(line);
        public static Token BangEqual(int line) => new BangEqual(line);
        public static Token Equal(int line) => new Equal(line);
        public static Token EqualEqual(int line) => new EqualEqual(line);
        public static Token Greater(int line) => new Greater(line);
        public static Token GreaterEqual(int line) => new GreaterEqual(line);
        public static Token Less(int line) => new Less(line);
        public static Token LessEqual(int line) => new LessEqual(line);
        public static Token Identifier(string identifier, int line) => new Identifier(identifier, identifier, line);
        public static Token String(string value, int line) => new String(value, value, line);
        public static Token Number(decimal value, int line) => new Number(value.ToString(), value, line);
        public static Token And(int line) => new And(line);
        public static Token Class(int line) => new Class(line);
        public static Token Else(int line) => new Else(line);
        public static Token False(int line) => new False(line);
        public static Token For(int line) => new For(line);
        public static Token Fun(int line) => new Fun(line);
        public static Token If(int line) => new If(line);
        public static Token Nil(int line) => new Nil(line);
        public static Token Or(int line) => new Or(line);
        public static Token Print(int line) => new Print(line);
        public static Token Return(int line) => new Return(line);
        public static Token Super(int line) => new Super(line);
        public static Token This(int line) => new This(line);
        public static Token True(int line) => new True(line);
        public static Token Var(int line) => new Var(line);
        public static Token While(int line) => new While(line);
        public static Token EOF() => new EOF();

        public static readonly IReadOnlyDictionary<string, Func<int, Token>> Keywords = new Dictionary<string, Func<int, Token>>
        {
            { "and", And },
            { "class", Class },
            { "else", Else },
            { "false", False },
            { "for", For },
            { "fun", Fun },
            { "if", If },
            { "nil", Nil },
            { "or", Or },
            { "print", Print },
            { "return", Return },
            { "super", Super },
            { "this", This },
            { "true", True },
            { "var", Var },
            { "while", While }
        };
    }

    public interface LexemeBearer { string Lexeme { get; } }
    internal abstract record ValueToken<T>(string Lexeme, T Value, int Line) : Token(Lexeme, Line), Literal<T> { }
    public interface Keyword : LexemeBearer { }
    public interface Unary : Operator { }
    public interface Binary : Operator { }
    public interface Literal<T> : LexemeBearer { T Value { get; } }
    public interface Parentheses : LexemeBearer { }
    internal interface Equality : Binary { }
    internal interface Comparison : Binary { }
    internal interface Term : Binary { }
    internal interface Factor : Binary { }
    public interface Operator : LexemeBearer { }

    internal record ParenLeft(int Line) : Token("(", Line), Parentheses { }
    internal record ParenRight(int Line) : Token(")", Line), Parentheses { }
    internal record BraceLeft(int Line) : Token("{", Line) { }
    internal record BraceRight(int Line) : Token("}", Line) { }
    internal record Comma(int Line) : Token(",", Line) { }
    internal record Dot(int Line) : Token(".", Line) { }
    internal record Minus(int Line) : Token("-", Line), Unary, Term { }
    internal record Plus(int Line) : Token("+", Line), Term { }
    internal record Semicolon(int Line) : Token(";", Line) { }
    internal record Slash(int Line) : Token("/", Line), Factor { }
    internal record Star(int Line) : Token("*", Line), Factor { }
    internal record Bang(int Line) : Token("!", Line), Unary { }
    internal record BangEqual(int Line) : Token("!=", Line), Equality { }
    internal record Equal(int Line) : Token("=", Line) { }
    internal record EqualEqual(int Line) : Token("==", Line), Equality { }
    internal record Greater(int Line) : Token(">", Line), Comparison { }
    internal record GreaterEqual(int Line) : Token(">=", Line), Comparison { }
    internal record Less(int Line) : Token("<", Line), Comparison { }
    internal record LessEqual(int Line) : Token("<=", Line), Comparison { }
    internal record Identifier(string Lexeme, string Value, int Line) : ValueToken<string>(Lexeme, Value, Line) { }
    internal record String(string Lexeme, string Value, int Line) : ValueToken<string>(Lexeme, Value, Line) { }
    internal record Number(string Lexeme, decimal Value, int Line) : ValueToken<decimal>(Lexeme, Value, Line) { }
    internal record And(int Line) : Token("and", Line) { }
    internal record Class(int Line) : Token("class", Line) { }
    internal record Else(int Line) : Token("else", Line) { }
    internal record False(int Line) : ValueToken<bool>("false", false, Line) { }
    internal record Fun(int Line) : Token("fun", Line) { }
    internal record For(int Line) : Token("for", Line) { }
    internal record If(int Line) : Token("if", Line) { }
    internal record Nil(int Line) : ValueToken<object?>("nil", null, Line) { }
    internal record Or(int Line) : Token("or", Line) { }
    internal record Print(int Line) : Token("print", Line) { }
    internal record Return(int Line) : Token("return", Line) { }
    internal record Super(int Line) : Token("super", Line) { }
    internal record This(int Line) : Token("this", Line) { }
    internal record True(int Line) : ValueToken<bool>("true", true, Line) { }
    internal record Var(int Line) : Token("var", Line) { }
    internal record While(int Line) : Token("while", Line) { }
    internal record EOF() : Token("EOF", 0);
}

#pragma warning restore IDE1006 // Naming Styles