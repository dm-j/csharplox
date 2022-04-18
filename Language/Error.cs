namespace Language
{
    public class ParseError : Exception
    {
        internal ParseError(string message) : base(message) { }
    }

    public class RuntimeError : Exception
    {
        public readonly Operator Token;

        internal RuntimeError(Operator token, string message) : base(message) 
        {
            Token = token;
        }
    }
}
