using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            List<Stew> stews = new List<Stew>
            {
                new Stew("Гродфуд", 2000, 25),
                new Stew("Барс", 2000, 25),
                new Stew("Совок", 2000, 24),
                new Stew("Йошкар-Олинский мясокомбинат", 2000, 24),
                new Stew("Вкусвилл", 2000, 23),
                new Stew("Микоян", 2000, 23),
                new Stew("Слонимский мясокомбинат", 2000, 22),
                new Stew("Кронидов", 2000, 22)
            };

            Console.WriteLine("Все банки тушенки: \n");

            foreach (Stew stew in stews)
                stew.ShowInfo();

            bool isCorrectInput = false;
            int inputYear;
            int minYearOfProduction = stews.Min(stew => stew.YearOfProduction);

            Console.WriteLine($"\nВведите интересующий вас год, начиная с {minYearOfProduction}: ");

            while (isCorrectInput == false)
            {
                if(int.TryParse(Console.ReadLine(), out inputYear))
                {
                    if(minYearOfProduction <= inputYear)
                    {
                        List<Stew> expiredStew = stews.Where(stew => inputYear > stew.YearOfProduction + stew.ShelfLife).ToList();

                        if(expiredStew.Count > 0)
                        {
                            Console.WriteLine("Все просроченные банки тушёнки: ");

                            foreach (var stew in expiredStew)
                                stew.ShowInfo();
                        }
                        else
                        {
                            Console.WriteLine("Нет просроченных банок тушенки. Можно есть всё!!!");
                        }

                        isCorrectInput = true;
                    }
                    else
                    {
                        Console.WriteLine($"Год должен быть больше, чем {minYearOfProduction}");
                    }
                }
                else
                {
                    Console.WriteLine("Нужно ввести число");
                }
            }
        }
    }

    class Stew
    {
        public Stew(string title, int yearOfProduction, int shelfLife)
        {
            Title = title;
            YearOfProduction = yearOfProduction;
            ShelfLife = shelfLife;
        }

        public string Title { get; private set; }
        public int YearOfProduction { get; private set; }
        public int ShelfLife { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Название: {Title}. Год производства: {YearOfProduction}. Срок годности: {ShelfLife}");
        }
    }
}