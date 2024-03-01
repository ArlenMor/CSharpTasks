using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[10];

            int bottomLimit = 10;
            int upLimit = 100;
            Random random = new Random();

            Console.WriteLine("Исходный массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(bottomLimit, upLimit);
                Console.Write($"{array[i]} ");
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                    if (array[j] > array[j + 1])
                    {
                        int tmp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = tmp;
                    }
            }

            Console.WriteLine();
            Console.WriteLine("Отсортированный массив: ");

            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]} ");
        }
    }
}