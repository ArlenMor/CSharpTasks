using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Queue<int> customers = new Queue<int>();

            int cashbox = 0;

            Random random = new Random();
            int maxCustomers = 10;
            int numbersOfCustomers = random.Next(1, maxCustomers + 1);

            int minSumPurchase = 1;
            int maxSumPurchase = 100;
            int numberOfCustomer;

            for (int i = 0; i < numbersOfCustomers; i++)
                customers.Enqueue(random.Next(minSumPurchase, maxSumPurchase + 1));

            Console.WriteLine("Очередь состоит из: ");
            foreach (var customer in customers)
                Console.WriteLine($"{customer}");

            Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadLine();

            numberOfCustomer = customers.Count;

            for (int i = 0; i < numberOfCustomer; i++)
            {
                Console.Clear();
                Console.WriteLine($"Пришёл покупатель. Сумма его покупки: {customers.Peek()}");
                Console.WriteLine($"Денег в кассе до покупки: {cashbox}");
                cashbox += customers.Dequeue();
                Console.WriteLine($"Денег в кассе после покупки: {cashbox}");

                Console.WriteLine($"Нажмите любую клавишу, чтобы принять следующего покупателя...");
                Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine($"Очередь опустела. Всего денег в кассе: {cashbox}\nНажмите любую клавишу, чтобы продолжить");
            Console.ReadLine();
        }
    }
}