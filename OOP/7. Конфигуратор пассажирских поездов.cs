using System;
using System.Collections.Generic;
using System.Threading;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            StationManager stationManager = new StationManager();

            stationManager.Work();
        }
    }

    class StationManager
    {
        private List<Train> _trains;

        public StationManager()
        {
            _trains = new List<Train>();
        }

        public void Work()
        {
            const char CreateTrainPlanCommand = '1';
            const char ExitCommand = '2';

            bool isWork = true;

            List<Wagon> activeWagons = new List<Wagon>();

            while (isWork)
            {
                Console.WriteLine("Конфигуратор пассажирских поездов!");

                if (_trains.Count != 0)
                {
                    for (int i = 0; i < _trains.Count; i++)
                    {
                        Console.Write($"{i + 1}.");
                        _trains[i].ShowInfo();
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("Введите команду: ");

                Console.WriteLine($"{CreateTrainPlanCommand} - создать план поезда");
                Console.WriteLine($"{ExitCommand} - закрыть программу");

                char userInput = Console.ReadKey().KeyChar;
                Console.WriteLine();

                Train train = new Train();

                switch (userInput)
                {
                    case CreateTrainPlanCommand:
                        _trains.Add(train.Create());
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Train
    {
        private List<Wagon> _wagons;
        private Path _path;

        public Train()
        {
            _wagons = new List<Wagon>();
            _path = null;
        }

        public int NumberOfPassengers
        {
            get
            {
                int numberOfPassengers = 0;

                foreach (var wagon in _wagons)
                    numberOfPassengers += wagon.Passangers;

                return numberOfPassengers;
            }
        }

        public Train Create()
        {
            _path = CreatePath();
            FillWagons();
            Send();

            return this;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Маршрут: {_path.Start} -> {_path.End}. Количество вагонов: {_wagons.Count}. Количество пассажиров: {NumberOfPassengers}");

            for (int i = 0; i < _wagons.Count; i++)
                Console.Write($"Вагон #{i + 1}: {_wagons[i].Passangers}/{_wagons[i].MaxPlaces}; ");
        }

        private Path CreatePath()
        {
            string startOfRoute = string.Empty;
            string endOfRoute = string.Empty;

            while (startOfRoute == string.Empty || endOfRoute == string.Empty)
            {
                Console.Write("Введите стартовую станцию поезда: ");
                startOfRoute = Utilites.InputNonEmptyString();

                Console.Write("Введите конечную станцию поезда: ");
                endOfRoute = Utilites.InputNonEmptyString();

                if (startOfRoute == endOfRoute)
                {
                    Console.WriteLine("Вы указали одинаковую станцию для старта и конца маршрута. Попробуйте ещё раз.");
                    startOfRoute = endOfRoute = string.Empty;
                }
            }

            return new Path(startOfRoute, endOfRoute);
        }

        private void FillWagons()
        {
            Console.WriteLine("Идёт продажа билетов, ждите...");
            Thread.Sleep(1500);

            int numberOfTickets = Utilites.RandomNumber(100, 500);
            int maxPlacesInWagon = 100;

            Console.WriteLine($"Было продано {numberOfTickets} билетов.");

            Console.WriteLine("Идёт посадка пассажиров, ждите");
            Thread.Sleep(1500);

            while (numberOfTickets > 0)
            {
                int freePlaces;

                if (numberOfTickets >= maxPlacesInWagon)
                {
                    numberOfTickets -= maxPlacesInWagon;
                    freePlaces = 0;
                }
                else
                {
                    freePlaces = maxPlacesInWagon - numberOfTickets;
                    numberOfTickets = 0;
                }

                _wagons.Add(new Wagon(maxPlacesInWagon, freePlaces));
            }

            Console.WriteLine($"Пассажиры успешно распределены по местам. Количество вагонов: {_wagons.Count}");
        }

        private void Send()
        {
            Console.WriteLine("Нажмите любую клавишу для отправки поезда");
            Console.ReadKey();
            Console.WriteLine("Поезд успешно отправлен");
        }
    }

    class Wagon
    {
        public Wagon(int maxPlaces, int freePlaces)
        {
            MaxPlaces = maxPlaces;
            FreePlaces = freePlaces;
        }

        public int MaxPlaces { get; private set; }
        public int FreePlaces { get; private set; }

        public int Passangers => MaxPlaces - FreePlaces;
    }

    class Path
    {
        public Path(string start, string end)
        {
            Start = start;
            End = end;
        }

        public string Start { get; }
        public string End { get; }
    }

    class Utilites
    {
        private static Random _random = new Random();

        public static string InputNonEmptyString()
        {
            string input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Вы ввели пустую строку. Попробуйте ещё раз: ");
                input = Console.ReadLine();
            }

            return input;
        }

        public static void ReadPositivInt(string input, out int number)
        {
            if (int.TryParse(input, out number) == false)
            {
                Console.WriteLine("Нужно ввести целое положительное число, а не строку.");
                return;
            }

            if (number <= 0)
                Console.WriteLine("Нужно ввести целое положительное число.");
        }

        public static int RandomNumber(int minNumber, int maxMunber)
        {
            return _random.Next(minNumber, maxMunber + 1);
        }
    }
}