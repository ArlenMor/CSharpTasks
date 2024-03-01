using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int queueSize = 0;
            int receptionTimeInMinutes = 10;
            int allWaitingTimeInMinutes = 0;
            int minutesInHour = 60;

            Console.Write("Унылое утро понедельника в больнице, Вы находите свой кабинет. Сколько людей Вы видете в очереди? ");
            queueSize = Convert.ToInt32(Console.ReadLine());

            allWaitingTimeInMinutes = receptionTimeInMinutes * queueSize;
            int partWaitingTimeInHour = allWaitingTimeInMinutes / minutesInHour;
            int partWaitingTimeInMinutes = allWaitingTimeInMinutes % minutesInHour;

            Console.WriteLine($"Ооооо нет... Мне тут сидеть вечные {partWaitingTimeInHour} час и {partWaitingTimeInMinutes} минут...");
        }
    }
}