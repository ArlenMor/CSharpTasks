using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int arraySize = 5;
            int[] array = new int[arraySize];

            int bottomLimit = 0;
            int upLimit = 10;
            int shiftToRight;
            Random random = new Random();

            Console.WriteLine("Исходный массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(bottomLimit, upLimit);
                Console.Write($"{array[i]} ");
            }

            Console.WriteLine("\nУкажите, насколько позиций вправо вы хотите передвинуть элементы массива: ");
            shiftToRight = Convert.ToInt32(Console.ReadLine());

            shiftToRight %= array.Length;

            Console.WriteLine($"Нужно сместиться вправао на {shiftToRight}");

            for (int shiftCounter = 0; shiftCounter < shiftToRight; shiftCounter++)
            {
                int tmp = array[array.Length - 1];

                for (int i = array.Length - 1; i != 0; i--)
                    array[i] = array[i - 1];

                array[0] = tmp;
            }

            Console.WriteLine("После сдвига: ");

            for (int i = 0; i < array.Length; i++)
                Console.Write($"{array[i]} ");
        }
    }
}