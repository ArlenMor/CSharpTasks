using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int number = ReadInt();
            Console.WriteLine($"Ваше число {number}");
        }

        private static int ReadInt()
        {
            bool isValidNumber = false;

            int number = 0;

            while (!isValidNumber)
            {
                Console.Write("Введите целое число: ");
                string inputNumber = Console.ReadLine();

                if (int.TryParse(inputNumber, out number))
                    isValidNumber = true;
                else
                    Console.WriteLine("Неверные данные. Попробуйте ещё раз.");
            }

            return number;
        }
    }
}