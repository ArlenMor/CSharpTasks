using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int arraySize = 30;
            int[] array = new int[arraySize];

            int bottomLimit = 0;
            int upLimit = 3;
            int mostReplyValue;
            int mostReplyCounter = 0;
            int currentCounter = 1;

            Random random = new Random();

            Console.WriteLine("Изначальный массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(bottomLimit, upLimit);
                Console.Write($"{array[i]} ");
            }

            Console.WriteLine();

            mostReplyValue = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == array[i - 1])
                {
                    currentCounter++;

                    if(currentCounter > mostReplyCounter)
                    {
                        mostReplyCounter = currentCounter;
                        mostReplyValue = array[i - 1];
                    }
                }
                else
                {
                    currentCounter = 1;
                }
            }

            Console.WriteLine($"Число {mostReplyValue} повторяется большее число раз подряд, а именно {mostReplyCounter} раз.");
        }
    }
}