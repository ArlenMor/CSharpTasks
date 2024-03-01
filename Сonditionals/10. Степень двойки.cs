using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main()
        {
            int bottomLimit = 1;
            int upLimit = 5;
            int power = 0;
            int baseNumber = 2;
            int currentNumber = 1;

            Random random = new Random();
            int number = random.Next(bottomLimit, upLimit);

            Console.WriteLine($"Ваше число {number}");

            while (number >= currentNumber)
            {
                power++;
                currentNumber = currentNumber * baseNumber;
            }

            Console.WriteLine($"Минимальная степень двойки, превосходящая заданное число: {power}");
        }
    }
}