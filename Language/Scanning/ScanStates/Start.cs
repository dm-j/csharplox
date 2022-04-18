namespace Language.Scanning
{
    internal class Start : ScanState
    {
        internal override ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line)
        {
            switch (c)
            {
                case '(':
                    tokens(Token.ParenLeft(line));
                    break;
                case ')':
                    tokens(Token.ParenRight(line));
                    break;
                case '{':
                    tokens(Token.BraceLeft(line));
                    break;
                case '}':
                    tokens(Token.BraceRight(line));
                    break;
                case ',':
                    tokens(Token.Comma(line));
                    break;
                case '.':
                    tokens(Token.Dot(line));
                    break;
                case '-':
                    tokens(Token.Minus(line));
                    break;
                case '+':
                    tokens(Token.Plus(line));
                    break;
                case ';':
                    tokens(Token.Semicolon(line));
                    break;
                case '*':
                    tokens(Token.Star(line));
                    break;
                case '!':
                    return Bang;
                case '=':
                    return Equal;
                case '<':
                    return Less;
                case '>':
                    return Greater;
                case '/':
                    return Slash;
                case '\n':
                    line++;
                    break;
                case '"':
                    return String;
                case '@':
                case '_':
                    return Identifier.Scan(c, tokens, errors, ref line);
                default:
                    if (char.IsWhiteSpace(c))
                    {
                        break;
                    }

                    if (char.IsDigit(c))
                    {
                        return Number.Scan(c, tokens, errors, ref line);
                    }

                    if (char.IsLetter(c))
                    {
                        return Identifier.Scan(c, tokens, errors, ref line);
                    }

                    errors($"Unexpected character {c} on line {line}");
                    break;
            }

            return this;
        }
    }
}
