using System;
using System.Collections.Generic;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            FishStore store = new FishStore();

            store.SellFish();  
        }
    }

    class FishFarmer
    {

    }

    class FishStore
    {
        private List<Fish> _fishTemplate;

        public FishStore()
        {
            _fishTemplate = new List<Fish>
            {
                new Fish("Судак", 14, 10, 200, 20, 25),
                new Fish("Окунь", 15, 11, 190, 22, 26),
                new Fish("Ерш", 10, 5, 50, 20, 40),
                new Fish("Щука", 15, 13, 150, 25, 25),
                new Fish("Карп", 20, 18, 160, 20, 30),
                new Fish("Карась", 10, 6, 200, 25, 50),
                new Fish("Язь", 14, 12, 250, 25, 40),
                new Fish("Красноперка", 19, 12, 225, 20, 30),
                new Fish("Налим", 24, 15, 300, 15, 30),
                new Fish("Стерлядь", 30, 25, 500, 10, 20),
                new Fish("Форель ручьевая", 12, 11, 350, 25, 60),
                new Fish("Плотва", 13, 10, 200, 25, 50),
                new Fish("Лещь", 13, 6, 50, 60, 80)
            };
        }

        public Fish SellFish()
        {
            ConsoleKey exitKey = ConsoleKey.Escape;

            Console.WriteLine("Добро пожаловать! Вот мои дорогие рыбоньки. Выбирай!\n");
            ShowAllFishes();
            Console.WriteLine($"\nВведи номер рыбы, которую хочешь купить, или нажми {exitKey} для выхода: ");

            int index;
            string userInput = Console.ReadLine();

            if (int.TryParse(Console.ReadLine(), out index))
            {
                if(index < _fishTemplate.Count && index >= 0)
                {
                    Fish chosenFish = _fishTemplate[index];
                    return new Fish(chosenFish.Name, chosenFish.AvarageLifeExpectancy, chosenFish.AvarageBestYearForSale,
                                        chosenFish.StartCost, chosenFish.IncreaseCoef, chosenFish.DecreaseCoef);
                }
                else
                {
                    Console.Write("Вы ввели что-то не то, попробуйте снова: ");
                }
            }else if (userInput == exitKey.ToString())
            {
                return null;
            }
            else
            {
                Console.Write("Нужно ввести номер из списка. Попробуйте ещё раз: ");
            }



            return null;
        }

        private void ShowAllFishes()
        {
            string indexString = "№";
            string nameString = "Название";
            string avarageLifeExpectancyString = "Средняя продолжительность жизни";
            string avarageBestYearForSaleString = "Средний лучший год для продажи";
            string costString = "Цена";

            string str = String.Format("|{0, -2}| {1,-20} | {2,-4} | {3,-31} | {4,-30} |"
                , indexString
                , nameString
                , costString
                , avarageLifeExpectancyString
                , avarageBestYearForSaleString);
            Console.WriteLine(str);

            for (int i = 0;i < _fishTemplate.Count; i++)
            {
                str = String.Format("|{0,-2}| {1,-20} | {2,-4} | {3,-31} | {4,-30} |"
                , i + 1
                , _fishTemplate[i].Name
                , _fishTemplate[i].Cost
                , _fishTemplate[i].AvarageLifeExpectancy
                , _fishTemplate[i].AvarageBestYearForSale);
                Console.WriteLine(str);
            }
        }
    }

    class Pond
    {
        List<Fish> fishes;

        public Pond(int capacity) 
        {
            fishes = new List<Fish>();
            Capacity = capacity;
        }

        public int Capacity { get; private set; }

        public void AddFish(Fish fish)
        {

        }

        public void RemoveFish(Fish fish)
        {

        }

        public void ShowInfo()
        {

        }
    }

    class Fish
    {
        private int _actualLifeExpectancy;
        private int _actualBestYearForSale;

        public Fish(string name, int avarageLifeExpectancy, int avarageBestYearForSale, float startCost, int increaseCoef, int decreaseCoef)
        {
            Name = name;
            AvarageLifeExpectancy = avarageLifeExpectancy;
            AvarageBestYearForSale = avarageBestYearForSale;
            StartCost = startCost;
            Cost = StartCost;
            IncreaseCoef = increaseCoef;
            DecreaseCoef = decreaseCoef;

            int rangeOfYear = 3;
            int varianYears = Utilites.RandomizeNumber(-rangeOfYear, rangeOfYear);

            _actualLifeExpectancy = AvarageLifeExpectancy + varianYears;
            _actualBestYearForSale = AvarageBestYearForSale + varianYears;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public float StartCost { get; private set; }
        public float Cost { get; private set; }
        public float Income => Cost - StartCost;
        public int AvarageLifeExpectancy { get; private set; }
        public int AvarageBestYearForSale { get; private set; }
        public float AvarageMaxCost => GetAvarageMaxCost();
        public int IncreaseCoef { get; private set; }
        public int DecreaseCoef { get; private set; }

        public bool TryLife()
        {
            return Age < _actualLifeExpectancy;
        }

        public void Life()
        {
            Age++;

            if(Age <= AvarageBestYearForSale)
                Cost += Cost * (IncreaseCoef / Utilites.HundredPercent);
            else 
                Cost -= Cost * (DecreaseCoef / Utilites.HundredPercent);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Количество лет: {Age}");
            Console.WriteLine($"Предполагаемое количество лет: {AvarageLifeExpectancy}");
            Console.WriteLine($"Лучший предполагаемый год для продажи: {AvarageBestYearForSale}");
            Console.WriteLine($"Текущая цена: {Cost}");
        }

        private float GetAvarageMaxCost()
        {
            return (float)(StartCost * Math.Pow(Convert.ToDouble(1 + (IncreaseCoef / Utilites.HundredPercent)), AvarageBestYearForSale));
        }
    }

    class Utilites
    {
        private static Random s_random = new Random();

        public static int RandomizeNumber(int minNumber, int maxMunber)
        {
            return s_random.Next(minNumber, maxMunber + 1);
        }

        public static float HundredPercent => 100f;
    }
}