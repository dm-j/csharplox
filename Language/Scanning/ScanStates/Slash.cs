namespace Language.Scanning
{
    internal class Slash : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '/':
                    return Comment;
                default:
                    tokens(Token.Slash(line));
                    return Start.Scan(c, tokens, errors, ref line);
            }
        }
    }
}
