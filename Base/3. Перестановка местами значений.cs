using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstName = "Быкадаров";
            string secondName = "Егор";

            Console.WriteLine($"firstName: {firstName}, secondName: {secondName}");

            string temp = firstName;
            firstName = secondName;
            secondName = temp;

            Console.WriteLine($"firstName: {firstName}, secondName: {secondName}");
        }
    }
}