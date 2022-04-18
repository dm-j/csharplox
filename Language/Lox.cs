using Language.AST;
using Language.Parsing;
using Language.Scanning;
using Language.Visitors;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class Lox
    {
        public bool HasError { get; private set; } = false;

        public void Run(string s)
        {
            Scanner scanner = new(s, Error);
            ImmutableList<Token> tokens = scanner.Scan();
            Parser parser = new(tokens, Error);
            Expression? expression = parser.Parse();

            if (HasError || expression is null)
                return;

            Visitor<string> visitor = new ToStringVisitor();
            Console.WriteLine(visitor.Visit(expression));
        }

        public void Run(IEnumerable<string> s)
        {
            Run(string.Join('\n', s));
        }

        private void Error(string s)
        {
            HasError = true;
            Console.Error.WriteLine(s);
        }
    }
}
