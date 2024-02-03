namespace PBTests
{
    class ProcessingClass
    {
        public static int Process(TextReader input, TextWriter output)
        {
            double usd = double.Parse(input.ReadLine());
            double bgn = usd * 1.79549;

            // Use WriteLine to ensure the correct newline format is used
            output.WriteLine(bgn);

            return 0;
        }

        static void Main(string[] args)
        {
            Process(Console.In, Console.Out);
        }
    }
}
