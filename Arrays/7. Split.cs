using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            string text;
            char splitSymbol = ' ';

            Console.Write("Введите текст: ");
            text = Console.ReadLine();

            string[] subStrings = text.Split(splitSymbol);

            foreach (string subString in subStrings)
                Console.WriteLine(subString);
        }
    }
}