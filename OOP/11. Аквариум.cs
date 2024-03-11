using System;
using System.Collections.Generic;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine("Эта игра - симмулятор рыбного фермера.");
            Console.WriteLine("Ваша задача: покупать рыбов подешевле, а продавать подороже!");
            Console.WriteLine("У каждой рыбы есть ожидаемый срок жизни и средний наилучший возраст для продажи.");
            Console.WriteLine("До лучшего года для продажи цена на рыбу будет расти, а после него - убывать.");
            Console.WriteLine("Например, есть рыба \"А\" у которой ожидаемый срок жизни 10, а лучший год для продажи - 5.");
            Console.WriteLine("Тогда цена рыбы \"А\" будет рости примерно до 5 лет, а после чего начнёт снижаться.");
            Console.WriteLine("Почему примерно? Да потому что в жизни всё несколько иначе...");
            Console.WriteLine("Рыба может уродиться сильная, мощная, мясистая!!! Тогда жить она будет больше, а лучший год для её продажи будет другой, нежели у средней рыбы.");
            Console.WriteLine("Также может появиться и худородная рыбёшка. Которая и проживёт меньше остальных, и денег принесёт меньше...");
            Console.WriteLine("Внимательно следите за ценой рыбы. Если она начнёт падать, значит рыба прошла свой \"пик цены\" и стоит от нёё избавляться. Продавайте её скорее!!!");
            Console.WriteLine("Ну всё, приятной игры. Для продолжения нажмите любую клавишу...");

            Console.ReadKey();
            Console.Clear();

            int maxPondCapacity = 15;
            int startMoney = 500;

            FishStore store = new FishStore();
            FishFarmer farmer = new FishFarmer(maxPondCapacity, startMoney);

            bool isWork = true;

            while (isWork)
            {
                const string NextCommand = "1";
                const string BuyFishCommand = "2";
                const string SellFishCommand = "3";
                const string ExitCommand = "4";

                farmer.ShowPondInfo();
                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"Количество денег: {farmer.Money}");
                Console.WriteLine($"{NextCommand} - Прожить год.");
                Console.WriteLine($"{BuyFishCommand} - Купить рыбу и добавить в пруд.");
                Console.WriteLine($"{SellFishCommand} - Продать рыбу.");
                Console.WriteLine($"{ExitCommand} - Закрыть программу.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case NextCommand:
                        farmer.FinishYear();
                        break;
                    case BuyFishCommand:
                        Console.Clear();
                        farmer.BuyFish(store);
                        break;
                    case SellFishCommand:
                        farmer.SellFish();
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Вы ввели что-то не то. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить..");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class FishFarmer
    {
        private Pond _pond;

        public FishFarmer(int maxPondCapacity, int startMoney)
        {
            _pond = new Pond(maxPondCapacity);
            Money = startMoney;
        }

        public float Money { get; private set; }

        public void FinishYear()
        {
            _pond.FinishYear();
        }

        public void BuyFish(FishStore store)
        {
            if (_pond.Capacity >= _pond.MaxCapacity)
            {
                Console.WriteLine("Пруд полностью наполнен. Больше рыб добавлять нельзя.");
                return;
            }

            Console.WriteLine($"Количество денег: {Money}");

            float money = Money;
            Fish fish = store.Sell(ref money);
            Money = money;

            if (fish == null)
                Console.WriteLine("Вы ушли ни с чем.");
            else
                _pond.AddFish(fish);
        }

        public void SellFish()
        {
            if(_pond.Capacity == 0)
            {
                Console.WriteLine("Нечего продавать. Пруд пуст.");
                return;
            }

            string exitCommand = "exit";

            Console.WriteLine($"Введите номер рыбы, которую хотите продать \n" +
                $"или введите \"{exitCommand}\" чтобы выйти из этого меню: ");

            int index;
            bool isCorrectInput = false;

            while (isCorrectInput == false)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out index))
                {
                    if (index <= _pond.Capacity && index > 0)
                    {
                        Fish chosenFish = _pond.GetFishByIndex(index - 1);

                        Money += chosenFish.Cost;

                        Console.WriteLine($"Рыба {chosenFish.Name} была успешно продана за {chosenFish.Cost}.");

                        _pond.RemoveFish(chosenFish);

                        isCorrectInput = true;
                    }
                    else
                    {
                        Console.Write("Вы ввели неверный номер, попробуйте снова: ");
                    }
                }
                else if (userInput == exitCommand)
                {
                    isCorrectInput = true;
                }
                else
                {
                    Console.Write("Нужно ввести номер из списка.\n" +
                        "Попробуйте ещё раз: ");
                }
            }
        }

        public void ShowPondInfo()
        {
            _pond.ShowInfo();
        }
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

        public Fish Sell(ref float buyersMoney)
        {
            string exitCommand = "exit";

            Console.WriteLine("Добро пожаловать! Вот мои дорогие рыбоньки. Выбирай!\n");
            ShowAllFishes();
            Console.WriteLine($"\nВведи номер рыбы, которую хочешь купить, или нажми {exitCommand} для выхода: ");

            int index;
            bool isCorrectInput = false;
            Fish fish = null;

            while (isCorrectInput == false)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out index))
                {
                    if (index <= _fishTemplate.Count && index > 0)
                    {
                        Fish chosenFish = _fishTemplate[index - 1];

                        if (buyersMoney >= chosenFish.Cost)
                        {
                            fish = new Fish(chosenFish.Name, chosenFish.AvarageLifeExpectancy, chosenFish.AvarageBestYearForSale,
                                            chosenFish.StartCost, chosenFish.IncreaseCoef, chosenFish.DecreaseCoef);

                            buyersMoney -= chosenFish.Cost;

                            isCorrectInput = true;
                        }
                        else
                        {
                            Console.WriteLine("У вас недотаточно денег для покупки этой рыбы.");
                            Console.Write($"Хотите вернуться к выбору? Введите что угодно, чтобы продолжить или \"{exitCommand}\" чтобы закончить покупки:");
                            userInput = Console.ReadLine();

                            if (userInput == exitCommand)
                                return null;
                        }
                    }
                    else
                    {
                        Console.Write("Вы ввели неверный номер, попробуйте снова: ");
                    }
                }
                else if (userInput == exitCommand)
                {
                    isCorrectInput = true;
                }
                else
                {
                    Console.Write("Нужно ввести номер из списка. Попробуйте ещё раз: ");
                }
            }

            return fish;
        }

        private void ShowAllFishes()
        {
            string indexString = "№";
            string nameString = "Название";
            string costString = "Цена";
            string avarageLifeExpectancyString = "Средняя продолжительность жизни";
            string avarageBestYearForSaleString = "Средний лучший год для продажи";
            string IncreaseCoefString = "Увеличение в цене за год";

            string str = String.Format("|{0, -2}| {1,-20} | {2,-4} | {3,-31} | {4,-30} | {5,-10}",
                indexString,
                nameString,
                costString,
                avarageLifeExpectancyString,
                avarageBestYearForSaleString,
                IncreaseCoefString);
            Console.WriteLine(str);

            for (int i = 0; i < _fishTemplate.Count; i++)
            {
                str = String.Format("|{0,-2}| {1,-20} | {2,-4} | {3,-31} | {4,-30} | {5, -10}",
                i + 1,
                _fishTemplate[i].Name,
                _fishTemplate[i].Cost,
                _fishTemplate[i].AvarageLifeExpectancy,
                _fishTemplate[i].AvarageBestYearForSale,
                _fishTemplate[i].IncreaseCoef);
                Console.WriteLine(str);
            }
        }
    }

    class Pond
    {
        private List<Fish> _fishes;

        public Pond(int maxCapacity)
        {
            _fishes = new List<Fish>();
            MaxCapacity = maxCapacity;
        }

        public int MaxCapacity { get; private set; }
        public int Capacity => _fishes.Count;

        public void AddFish(Fish fish)
        {
            if (Capacity < MaxCapacity)
            {
                if (fish != null)
                    _fishes.Add(fish);
                else
                    Console.WriteLine("Нечего добавлять в пруд.");
            }
            else
            {
                Console.WriteLine("Превышена вместимость пруда.");
            }
        }

        public void RemoveFish(Fish remoteFish)
        {
            bool isContain = false;

            foreach (Fish fish in _fishes)
                if (fish == remoteFish)
                    isContain = true;

            if (isContain)
                _fishes.Remove(remoteFish);
            else
                Console.WriteLine("Такой рыбы нет в пруду.");
        }

        public void FinishYear()
        {
            Stack<int> remoteIndexes = new Stack<int>();

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].TryLife())
                {
                    _fishes[i].Life();
                }
                else
                {
                    Console.WriteLine($"{_fishes[i].Name} умер. Она стоила {_fishes[i].Cost}.");
                    remoteIndexes.Push(i);
                }
            }

            Console.WriteLine($"За год умерло {remoteIndexes.Count} рыб.");

            while(remoteIndexes.Count != 0)
            {
                int remoteIndex = remoteIndexes.Pop();
                _fishes.RemoveAt(remoteIndex);
            }
        }

        public Fish GetFishByIndex(int index)
        {
            if(index >= 0 && index < _fishes.Count)
                return _fishes[index];
            else
                Console.WriteLine("Индекс не в пределах диапазона.");

            return null;
        }

        public void ShowInfo()
        {
            int cursorPositionLeft = 50;
            int cursorPositionTop = 0;

            for (int i = 0; i < _fishes.Count; i++, cursorPositionTop++)
            {
                Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
                Console.WriteLine($"Рыба номер {i + 1}");
                cursorPositionTop++;
                _fishes[i].ShowInfo(ref cursorPositionLeft, ref cursorPositionTop);
                Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
                Console.Write("----------");
            }
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

            if (Age <= _actualBestYearForSale)
                Cost += Cost * (IncreaseCoef / Utilites.HundredPercent);
            else
                Cost -= Cost * (DecreaseCoef / Utilites.HundredPercent);
        }

        public void ShowInfo(ref int cursorPositionLeft, ref int cursorPositionTop)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Название: {Name}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Количество лет: {Age}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Предполагаемое количество лет: {AvarageLifeExpectancy}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Лучший предполагаемый год для продажи: {AvarageBestYearForSale}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Текущая цена: {Cost}");
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