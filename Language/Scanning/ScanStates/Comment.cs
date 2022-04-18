namespace Language.Scanning
{
    internal class Comment : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '\n':
                    line++;
                    return Start;
                default:
                    break;
            }

            return this;
        }
    }
}
