using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            int age;
            string zodiac;
            string workplace;

            Console.Write("Ваше имя: ");
            name = Console.ReadLine();
            Console.Write("Ваш возраст: ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ваш знак зодиака: ");
            zodiac = Console.ReadLine();
            Console.Write("Ваше место работы: ");
            workplace = Console.ReadLine();

            Console.WriteLine($"Вас зовут {name}, Вам {age} лет, Ваш знак зодиака {zodiac} и Ваше место работы {workplace}. Теперь Вы у нас на карандаше.");
        }
    }
}