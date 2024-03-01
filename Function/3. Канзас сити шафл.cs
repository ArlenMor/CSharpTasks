using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int arraySize = 10;
            int[] numbers = new int[arraySize];

            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = i;

            PrintArray(numbers);
            Console.WriteLine();

            ShuffleArray(numbers);

            PrintArray(numbers);
            Console.WriteLine();
        }


        private static void ShuffleArray(int[] array)
        {
            Random random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i);

                int tmp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = tmp;
            }
        }

        private static void PrintArray(int[] array)
        {
            foreach(var item in array)
                Console.Write($"{item} ");
        }
    }
}