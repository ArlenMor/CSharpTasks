using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int sizeForFirstArray = 3;
            int sizeForSecondArray = 2;

            string[] firstArray = new string[sizeForFirstArray];
            string[] secondArray = new string[sizeForSecondArray];

            List<string> resultList = new List<string>();

            Console.WriteLine("Заполните первый массив: ");
            FillArray(firstArray);
            Console.Clear();

            Console.WriteLine("Заполните второй массив: ");
            FillArray(secondArray);
            Console.Clear();

            Console.Write("Первый массив: ");
            PrintArrayInRow(firstArray);

            Console.WriteLine();

            Console.Write("Второй массив: ");
            PrintArrayInRow(secondArray);

            AddUniqueValues(firstArray, resultList);
            AddUniqueValues(secondArray, resultList);

            Console.WriteLine("\nРезультат: ");

            PrintArrayInRow(resultList.ToArray());

            Console.WriteLine();
        }

        private static void FillArray(string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                strings[i] = Console.ReadLine();
        }

        private static void PrintArrayInRow(string[] array)
        {
            foreach (string item in array)
                Console.Write($"\"{item}\" ");
        }
        private static void AddUniqueValues(string[] inputStrings, List<string> resultList)
        {
            for (int i = 0; i < inputStrings.Length; i++)
                if (resultList.Contains(inputStrings[i]) == false)
                    resultList.Add(inputStrings[i]);
        }
    }
}