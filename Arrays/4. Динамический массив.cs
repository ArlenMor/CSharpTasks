using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            const string SumCommand = "sum";
            const string ExitCommand = "exit";

            string userCommand;

            int arraySize;

            bool isOpen = true;

            Console.Write("Привет! Введите количество элементов массива: ");
            arraySize = Convert.ToInt32(Console.ReadLine());

            int[] array = new int[arraySize];

            Console.WriteLine("Введите элементы массива, используя клавишу Enter: ");
            
            for (int i = 0; i < array.Length; i++) 
                array[i] = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            while (isOpen)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Элементы массива сейчас:");
                
                foreach (int number in array)
                    Console.Write(number + " ");

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Отлично, теперь нам есть с чем работать. Вы можете использовать следующие комманды:");
                Console.WriteLine($"-> \"{SumCommand}\" - вывести сумму всех веденных чисел.");
                Console.WriteLine($"-> \"{ExitCommand}\" - выйти из программы.");
                Console.WriteLine($"Вы также можете ввести любое число, оно добавиться в массив.");

                switch (userCommand = Console.ReadLine().ToLower())
                {
                    case SumCommand:
                        int sumOfElement = 0;
                        
                        foreach (int number in array)
                            sumOfElement += number;

                        Console.WriteLine($"Сумма чисел массива сейчас: {sumOfElement}");
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        int newNumber = Convert.ToInt32(userCommand);
                        int[] tempArray = new int[array.Length + 1];
                        
                        for(int i = 0; i < array.Length; i++)
                            tempArray[i] = array[i];

                        tempArray[tempArray.Length - 1] = newNumber;
                        array = tempArray;
                        break;
                }

                Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}