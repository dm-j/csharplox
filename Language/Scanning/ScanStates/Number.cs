namespace Language.Scanning
{
    internal class Number : ScanState
    {
        private readonly StringAccumulator Value = new();

        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    Value.Add(c);
                    break;
                case ',':
                case '_':
                    break;
                default:
                    if (decimal.TryParse(Value, out decimal value))
                    {
                        tokens(Token.Number(value, line));
                    }
                    else
                    {
                        errors($"Invalid number {Value} on line {line}");
                    }

                    return Start.Scan(c, tokens, errors, ref line);
            }

            return this;
        }
    }
}
