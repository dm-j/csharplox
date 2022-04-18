using System.Collections.Immutable;

namespace Language.Scanning
{
    public class Scanner
    {
        private readonly string Input;
        private readonly Action<string> Error;

        public Scanner(string input, Action<string> error)
        {
            Input = input;
            Error = error;
        }

        public ImmutableList<Token> Scan()
        {
            List<Token> tokens = new();
            int line = 0;

            ScanState state = ScanState.Start;

            foreach (char c in Input)
            {
                state = state.Scan(c, tokens.Add, Error, ref line);
            }

            tokens.Add(Token.EOF());

            return tokens.ToImmutableList();
        }
    }
}
