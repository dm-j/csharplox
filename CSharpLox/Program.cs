using Language;
using System.Text;

while (true)
{
    Console.WriteLine("Enter code here. Enter a blank line to submit.");
    StringBuilder sb = new();
    string? s;
    do
    {
        s = Console.ReadLine();
        sb.AppendLine(s);
    } while (!string.IsNullOrWhiteSpace(s));

    Lox lox = new();

    lox.Run(sb.ToString());

    Console.WriteLine();
}
