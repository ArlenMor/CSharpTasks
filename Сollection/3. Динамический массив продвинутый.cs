using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            const string SumCommand = "sum";
            const string ExitCommand = "exit";

            List<int> numbers = new List<int>();

            string userCommand;
            int sum = 0;

            bool isOpen = true;

            while (isOpen)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Элементы массива сейчас:");

                foreach (int number in numbers)
                    Console.Write(number + " ");

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Вы можете использовать следующие комманды:");
                Console.WriteLine($"-> \"{SumCommand}\" - вывести сумму всех веденных чисел.");
                Console.WriteLine($"-> \"{ExitCommand}\" - выйти из программы.");
                Console.WriteLine($"Вы также можете ввести любое число, оно добавиться в массив.");

                switch (userCommand = Console.ReadLine().ToLower())
                {
                    case SumCommand:
                        sum = GetListSum(numbers);
                        Console.WriteLine($"Сумма чисел массива сейчас: {sum}");
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        AddNewElement(numbers, userCommand);
                        break;
                }

                Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static int GetListSum(List<int> numbers)
        {
            int result = 0;

            foreach(int number in numbers)
                result += number;

            return result;
        }

        public static void AddNewElement(List<int> numbers, string number)
        {
            int newNumber;

            if (int.TryParse(number, out newNumber))
                numbers.Add(newNumber);
            else
                Console.WriteLine("Вы можете вводить только команды и целые числа.\nПопробуйте ещё раз");
        }
    }
}