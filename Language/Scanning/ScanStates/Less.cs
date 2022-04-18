namespace Language.Scanning
{
    internal class Less : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '=':
                    tokens(Token.LessEqual(line));
                    break;
                default:
                    tokens(Token.Less(line));
                    return Start.Scan(c, tokens, errors, ref line);
            }

            return Start;
        }
    }
}
