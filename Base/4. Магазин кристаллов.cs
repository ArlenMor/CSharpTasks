using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int gold = 0;
            int crystals = 0;
            int crystalPrice = 5;         

            Console.Write("Сколько у Вас золота? ");
            gold = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Сейчас у Вас {gold} золота и {crystals} кристалов.");
            Console.WriteLine($"1 Кристал сейчас стоит {crystalPrice} золота.");

            Console.Write("Сколько кристалов вы хотите купить: ");
            crystals = Convert.ToInt32(Console.ReadLine());
            gold -= crystals * crystalPrice;

            Console.WriteLine($"С Вами приятно иметь дело! Сейчас у Вас {gold} золота и {crystals} кристалов. А Вы мне, случайно, ничего не должны..?");
        }
    }
}