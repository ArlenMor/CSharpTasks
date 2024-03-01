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

            Console.WriteLine();
            Console.WriteLine("Локальные максимумы: ");

            if (array[0] > array[1])
                Console.Write(array[0] + " ");

            for (int i = 1; i < array.Length - 1; i++)
                if (array[i] > array[i + 1] && array[i] > array[i - 1])
                    Console.Write(array[i] + " ");

            if (array[array.Length - 1] > array[array.Length - 2])
                Console.Write(array[array.Length - 1] + " ");
        }
    }
}