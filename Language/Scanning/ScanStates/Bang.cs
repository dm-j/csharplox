namespace Language.Scanning
{
    internal class Bang : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '=':
                    tokens(Token.BangEqual(line));
                    break;
                default:
                    tokens(Token.Bang(line));
                    return Start.Scan(c, tokens, errors, ref line);
            }

            return Start;
        }
    }
}
