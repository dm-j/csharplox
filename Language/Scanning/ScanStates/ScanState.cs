namespace Language.Scanning
{
    internal abstract class ScanState
    {
        internal abstract ScanState Scan(char c, Action<Token> tokens, Action<string> errors, ref int line);

        internal static ScanState Start { get; } = new Start();
        internal static ScanState Bang { get; } = new Bang();
        internal static ScanState Equal { get; } = new Equal();
        internal static ScanState Greater { get; } = new Greater();
        internal static ScanState Less { get; } = new Less();
        internal static ScanState Slash { get; } = new Slash();
        internal static ScanState Comment { get; } = new Comment();
        internal static ScanState String => new String();
        internal static ScanState Number => new Number();
        internal static ScanState Identifier => new Identifier();
    }
}
