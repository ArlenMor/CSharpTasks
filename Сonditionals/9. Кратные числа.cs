using System;

namespace IJunior
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();

            int bottomRandLimit = 1;
            int upRandLimit = 27;
            int number = random.Next(bottomRandLimit, upRandLimit + 1);
            int numberOfMultiples = 0;
            int leastDivisor = 100;
            int highestDivisor = 999;

            Console.WriteLine($"Ваше число {number}");

            for (int i = number; i < highestDivisor; i += number)
                if (i >= leastDivisor)
                    numberOfMultiples++;

            Console.WriteLine($"Количество натуральных чисел, кратных {number}, в диапазоне от {leastDivisor} до {highestDivisor} равно {numberOfMultiples}");
        }
    }
}