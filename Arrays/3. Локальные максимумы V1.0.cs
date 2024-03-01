using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[30];

            int bottomLimit = 1;
            int upLimit = 10;

            Random random = new Random();

            Console.WriteLine("Изначальный масив:");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(bottomLimit, upLimit);
                Console.Write(array[i] + " ");
            }

            Console.WriteLine("\nВыделенные локальные максимумы:");

            for (int i = 0; i < array.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                if (i == 0)
                {
                    if (array[i] > array[i + 1])
                    {
                        Console.Write(array[i] + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(array[i] + " ");
                    }
                }
                else if (i == array.Length - 1)
                {
                    if (array[i] > array[i - 1])
                    {
                        Console.Write(array[i] + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(array[i] + " ");
                    }
                }
                else if (array[i] > array[i + 1] && array[i] > array[i - 1])
                {
                    Console.Write(array[i] + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(array[i] + " ");
                }
            }
            Console.ResetColor();
        }
    }
}