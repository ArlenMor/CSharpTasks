using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string message;
            string exitCommand = "exit";

            Console.WriteLine($"Привет. Я работаю до тех пор, пока ты не напишешь {exitCommand}. Нет, я серьёзно, попробуй)");
            message = Console.ReadLine();

            while (message != exitCommand)
            {
                Console.WriteLine($"Ты что, читать не умеешь? Я же сказал: \"Пока ты не напишешь {exitCommand}\"... Пробуй ещё...");
                message = Console.ReadLine();
            }

            Console.WriteLine("Ну вот, даже ты справился) Всё было просто, верно?)");
        }
    }
}