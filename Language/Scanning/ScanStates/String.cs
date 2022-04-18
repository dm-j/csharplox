namespace Language.Scanning
{
    internal class String : ScanState
    {
        private readonly StringAccumulator Value = new();

        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '"':
                    tokens(Token.String(Value, line));
                    return Start;
                case '\n':
                    errors($"Unterminated string on line {line}");
                    return Start;
                default:
                    Value.Add(c);
                    break;
            }

            return this;
        }
    }
}
