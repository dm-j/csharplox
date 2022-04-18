namespace Language
{
    internal class StringAccumulator
    {
        private readonly List<char> Chars = new();

        internal StringAccumulator() { }

        internal void Add(char c)
        {
            Chars.Add(c);
        }

        public override string ToString()
        {
            return new string(Chars.ToArray());
        }

        public static implicit operator string(StringAccumulator sa) =>
            sa.ToString();
    }
}
