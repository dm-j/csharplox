using System.Collections.Immutable;

namespace Language
{
    internal class TokenStream
    {
        private readonly ImmutableArray<Token> Tokens;
        private int index = 0;

        internal TokenStream(IEnumerable<Token> tokens)
        {
            Tokens = tokens.ToImmutableArray();
        }

        public Token Advance()
        {
            if (!IsAtEnd())
            {
                index++;
            }

            return Previous();
        }

        public bool IsAtEnd()
        {
            return Peek() is EOF;
        }

        public Token Peek()
        {
            return Tokens[index];
        }

        public Token Previous()
        {
            return Tokens[index - 1];
        }

        public bool Match<TToken>()
        {
            if (Check<TToken>())
            {
                Advance();
                return true;
            }

            return false;
        }

        public bool Check<TToken>()
        {
            return !IsAtEnd() && Peek() is TToken;
        }

        public Token Consume<TToken>(string message)
        {
            if (Check<TToken>())
            {
                return Advance();
            }

            throw new ParseError($"Expected {typeof(TToken).Name}, found {Peek().GetType().Name}. {message}: {Peek()}");
        }
    }
}
