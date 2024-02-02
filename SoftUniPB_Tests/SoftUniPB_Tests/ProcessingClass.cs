using System;

class ProcessingClass
{
    public static int Process()
    {
        string destination;
        double ticketPrice = 0;
        destination = Console.ReadLine();

        while (destination != "End")
        {
            ticketPrice = Convert.ToDouble(Console.ReadLine());
            double collectedMoney = 0;

            while (collectedMoney < ticketPrice)
            {
                double earnedMoney;
                earnedMoney = Convert.ToDouble(Console.ReadLine());
                collectedMoney += earnedMoney;
            }

            if (collectedMoney >= ticketPrice)
            {
                Console.WriteLine($"Going to {destination}!");
            }

            destination = Console.ReadLine();
        }

        return 0;
    }

    static void Main(string[] args)
    {
        Process();
    }
}

