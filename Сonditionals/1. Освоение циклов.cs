using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string repeatMessage;
            int numberOfRepetitions;

            Console.Write("Привет. Я робот повторюша. Напиши то, что мне нужно повторять: ");
            repeatMessage = Console.ReadLine();

            Console.Write("Погоди, а сколько раз повторять-то? Ну ка напиши: ");
            numberOfRepetitions = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ну вот, теперь всё понятно. Я погнал)");

            for (int i = 0; i < numberOfRepetitions; i++)
                Console.WriteLine(repeatMessage);

            Console.WriteLine("Фууух. Устал повторять. Всё, пока, я отдыхать ._.");
        }
    }
}