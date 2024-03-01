using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int upperLimit = 100;
            int randomNumber = random.Next(upperLimit + 1);
            int sum = 0;
            int dividedEntirelyFirst = 3;
            int dividedEntirelySecond = 5;

            Console.WriteLine($"Надо же, ты зарандомил {randomNumber}. В нумерологии оно наверняка приносит счастье)");
            
            for (int i = 0; i < randomNumber; i++)
            {
                if (i % dividedEntirelyFirst == 0 || i % dividedEntirelySecond == 0)
                {
                    sum += i;
                    Console.Write($"{i} ");
                }
            }

            Console.WriteLine($"\nСумма чисел равна: {sum}");
        }
    }
}