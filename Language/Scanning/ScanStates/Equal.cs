namespace Language.Scanning
{
    internal class Equal : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '=':
                    tokens(Token.EqualEqual(line));
                    break;
                default:
                    tokens(Token.Equal(line));
                    return Start.Scan(c, tokens, errors, ref line);
            }

            return Start;
        }
    }
}
