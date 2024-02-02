using System;

class ProcessingClass
{
    public static int Process() { 
    double usd = double.Parse(Console.ReadLine());
    double bgn = usd * 1.79549;
    Console.WriteLine(bgn);
        return 0;
        }
    static void Main(string[] args)
    {
        Process();
    }
}

