namespace Language.Scanning
{
    internal class Identifier : ScanState
    {
        private readonly StringAccumulator Value = new();

        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            if (char.IsLetterOrDigit(c) || c == '_')
            {
                Value.Add(c);
                return this;
            }

            if (Token.Keywords.TryGetValue(Value, out Func<int, Token>? make))
            {
                tokens(make(line));
            }
            else
            {
                tokens(Token.Identifier(Value, line));
            }

            return Start.Scan(c, tokens, errors, ref line);
        }
    }
}
