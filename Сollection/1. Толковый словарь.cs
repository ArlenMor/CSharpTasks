using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            const string ExitCommand = "Закрыть журнал";

            string userCommand;

            bool isOpen = true;

            Dictionary<string, string> autoMechanicsMagazine = new Dictionary<string, string>
            {
                { "двигатель", "Устройство, преобразующее какой-либо вид энергии в механическую работу" },
                { "шасси", "Рама, на которой укреплены кузов, двигатель, все механизмы и детали" },
                { "кузов автомобиля", "Часть автомобиля или другого транспортного средства, предназначенная для размещения пассажиров и груза" },
                { "трансмиссия", "Совокупность механизмов для передачи движения (вращения) от двигателя к рабочим частям станков" }
            };

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine($"Вы открыли журнал начинающего автомеханика. В нём есть такие разделы, как:");

                foreach (string key in autoMechanicsMagazine.Keys)
                    Console.WriteLine($"{key}");

                Console.WriteLine($"Чтобы закрыть журнал, введите команду: {ExitCommand}");

                Console.Write("О чем вы хотите узнать больше? Напишите название раздела: ");

                userCommand = Console.ReadLine().ToLower();
                if (userCommand == ExitCommand.ToLower())
                    isOpen = false;
                else if (autoMechanicsMagazine.ContainsKey(userCommand))
                    Console.WriteLine($"В разделе \"{userCommand}\" пишут: {autoMechanicsMagazine[userCommand]}");
                else
                    Console.WriteLine("Нет такого раздела.");

                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadLine();
            }
        }
    }
}