using System;
using System.Collections.Generic;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            int maxAquariumCapacity = 15;

            FishTemplate template = new FishTemplate();
            AquariumOwner farmer = new AquariumOwner(maxAquariumCapacity);

            bool isWork = true;

            while (isWork)
            {
                const string NextCommand = "1";
                const string AddFishCommand = "2";
                const string RemoveFishCommand = "3";
                const string ExitCommand = "4";

                farmer.ShowPondInfo();
                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"{NextCommand} - Прожить год.");
                Console.WriteLine($"{AddFishCommand} - Добавить рыбу в аквариум.");
                Console.WriteLine($"{RemoveFishCommand} - Убрать рыбу из аквариума.");
                Console.WriteLine($"{ExitCommand} - Закрыть программу.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case NextCommand:
                        farmer.FinishYear();
                        break;
                    case AddFishCommand:
                        Console.Clear();
                        farmer.AddFish(template);
                        break;
                    case RemoveFishCommand:
                        farmer.RemoveFish();
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

    class AquariumOwner
    {
        private Aquarium _aquarium;

        public AquariumOwner(int maxAquariumCapacity)
        {
            _aquarium = new Aquarium(maxAquariumCapacity);
        }

        public void FinishYear()
        {
            _aquarium.FinishYear();
        }

        public void AddFish(FishTemplate template)
        {
            if (_aquarium.Capacity >= _aquarium.MaxCapacity)
            {
                Console.WriteLine("Аквариум полностью наполнен. Больше рыб добавлять нельзя.");
                return;
            }

            Fish fish = template.Choose();

            if (fish == null)
                Console.WriteLine("Вы ушли ни с чем.");
            else
                _aquarium.AddFish(fish);
        }

        public void RemoveFish()
        {
            string exitCommand = "exit";

            Console.WriteLine("\nВведи номер рыбы, которую хотите изъять, \n" +
                $"или нажми {exitCommand} для выхода: ");

            int index;
            bool isCorrectInput = false;

            while (isCorrectInput == false)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out index))
                {
                    if (index <= _aquarium.Capacity && index > 0)
                    {
                        Fish chosenFish = _aquarium.GetFishByIndex(index - 1);

                        _aquarium.RemoveFish(chosenFish);

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
                    Console.Write("Нужно ввести номер из списка. Попробуйте ещё раз: ");
                }
            }
        }

        public void ShowPondInfo()
        {
            _aquarium.ShowInfo();
        }
    }

    class FishTemplate
    {
        private List<Fish> _fishTemplate;

        public FishTemplate()
        {
            _fishTemplate = new List<Fish>
            {
                new Fish("Судак", 14),
                new Fish("Окунь", 15),
                new Fish("Ерш", 10),
                new Fish("Щука", 15),
                new Fish("Карп", 20),
                new Fish("Карась", 10),
                new Fish("Язь", 14),
                new Fish("Красноперка", 19),
                new Fish("Налим", 24),
                new Fish("Стерлядь", 30),
                new Fish("Форель ручьевая", 12),
                new Fish("Плотва", 13),
                new Fish("Лещь", 13)
            };
        }

        public Fish Choose()
        {
            string exitCommand = "exit";

            Console.WriteLine("Какую рыбу Вы хотите добавить?\n");
            ShowAllFishes();
            Console.WriteLine("\nВведи номер рыбы, которую хотите добавить, \n" +
                $"или нажми {exitCommand} для выхода: ");

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

                        fish = new Fish(chosenFish.Name, chosenFish.AvarageMaxAge);

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
                    Console.Write("Нужно ввести номер из списка. Попробуйте ещё раз: ");
                }
            }

            return fish;
        }

        private void ShowAllFishes()
        {
            string indexString = "№";
            string nameString = "Название";
            string liveString = "Продолжительность жизни";

            string outputString = String.Format("|{0, -2}| {1,-20} | {2,-20}",
                indexString,
                nameString,
                liveString);
            Console.WriteLine(outputString);

            for (int i = 0; i < _fishTemplate.Count; i++)
            {
                outputString = String.Format("|{0,-2}| {1,-20} | {2,-20}",
                i + 1,
                _fishTemplate[i].Name,
                _fishTemplate[i].AvarageMaxAge);
                Console.WriteLine(outputString);
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;

        public Aquarium(int maxCapacity)
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
                    Console.WriteLine("Нечего добавлять в аквариум.");
            }
            else
            {
                Console.WriteLine("Превышена вместимость аквариума.\n" +
                    "Рыба не была добавлена");
            }
        }

        public void RemoveFish(Fish remoteFish)
        {
            bool isContain = false;

            foreach (Fish fish in _fishes)
                if (fish == remoteFish)
                {
                    isContain = true;
                    break;
                }
                
            if (isContain)
                _fishes.Remove(remoteFish);
            else
                Console.WriteLine("Такой рыбы нет в аквариуме.");
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
                    Console.WriteLine($"{_fishes[i].Name} умер. Ей было {_fishes[i].Age} лет.");
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
            int cursorPositionLeft = 80;
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
        private int _maxAge;
        public Fish(string name, int maxAge)
        {
            Name = name;
            AvarageMaxAge = maxAge;

            Age = 0;

            int rangeOfLiveYear = 3;
            _maxAge = maxAge + Utilites.RandomizeNumber(-rangeOfLiveYear, rangeOfLiveYear);
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public int AvarageMaxAge { get; private set; }

        public bool TryLife()
        {
            return Age <= _maxAge;
        }

        public void Life()
        {
            Age++;
        }

        public void ShowInfo(ref int cursorPositionLeft, ref int cursorPositionTop)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Название: {Name}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
            Console.Write($"Количество лет: {Age}");
        }
    }

    class Utilites
    {
        private static Random s_random = new Random();

        public static int RandomizeNumber(int minNumber, int maxMunber)
        {
            return s_random.Next(minNumber, maxMunber + 1);
        }

        public static float PercentageConverter => 100f;
    }
}