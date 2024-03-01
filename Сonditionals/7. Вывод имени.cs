using System;

namespace IJunior
{
    class Program
    {
        static void Main()
        {
            string userName;
            string userNameWithDecorate;
            //string decorateString = String.Empty;

            char decorateSymbol;

            int numberOfSymbolInUserName;

            Console.Write("Введите своё имя: ");
            userName = Console.ReadLine();
            numberOfSymbolInUserName = userName.Length;

            Console.Write("Введите декоративный символ: ");
            decorateSymbol = Console.ReadKey().KeyChar;
            Console.WriteLine();

            userNameWithDecorate = decorateSymbol + userName + decorateSymbol;

            string decorateString = new string(decorateSymbol, userNameWithDecorate.Length);

          /*for (int i = 0; i < userNameWithDecorate.Length; i++)
                decorateString += decorateSymbol;*/

            Console.WriteLine($"{decorateString}");
            Console.WriteLine($"{userNameWithDecorate}");
            Console.WriteLine($"{decorateString}");
        }
    }
}