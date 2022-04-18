namespace Language.Scanning
{
    internal class Greater : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '=':
                    tokens(Token.GreaterEqual(line));
                    break;
                default:
                    tokens(Token.Greater(line));
                    return Start.Scan(c, tokens, errors, ref line);
            }

            return Start;
        }
    }
}
