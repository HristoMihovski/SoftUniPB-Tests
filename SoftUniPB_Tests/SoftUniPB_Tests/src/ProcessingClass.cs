using System;
using System.IO;

class ProcessingClass
{
    public static int Process(TextReader input, TextWriter output) { 
        double usd = double.Parse(input.ReadLine());
        double bgn = usd * 1.79549;
        output.WriteLine(bgn);
        return 0;
    }
    static void Main(string[] args)
    {
        Process(Console.In, Console.Out);
    }
}
