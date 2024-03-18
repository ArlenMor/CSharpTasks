using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            CarService carService = new CarService(10000);

            carService.Work();
        }
    }

    class CarService
    {
        private DetailsStorage _storage;
        private Queue<Client> _clients;

        public CarService(int startMoney)
        {
            Money = startMoney;
            _storage = new DetailsStorage();
            _clients = new Queue<Client>();
            CreateClientQueue();
        }

        public int Money { get; private set; }

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                const string AcceptClientCommand = "1";
                const string CheckStorageCommand = "2";
                const string ExitCommand = "3";

                Console.WriteLine(">>> Автосервис <<<");
                Console.WriteLine($"Количество денег: {Money}");
                Console.WriteLine("-------------------");
                Console.WriteLine($"{AcceptClientCommand}. Позвать следующего клиента.");
                Console.WriteLine($"{CheckStorageCommand}. Посмотреть склад деталей.");
                Console.WriteLine($"{ExitCommand}. Закрыть программу.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AcceptClientCommand:
                        AcceptClient(_clients.Dequeue());
                        break;
                    case CheckStorageCommand:
                        break;
                    case ExitCommand:
                        break;
                    default:
                        Console.WriteLine("Вы ввели что-то не то. Попробуйте ещё раз...");
                        break;
                }
            }
        }

        private void CreateClientQueue()
        {
            int maxClients = 6;
            int minClients = 1;

            ClientCreator clientsTemplate = new ClientCreator();

            for (int i = minClients; i < maxClients; i++)
                _clients.Enqueue(clientsTemplate.GetRandomizeClient());
        }

        private void AcceptClient(Client client)
        {
            Console.Clear();

            bool isProcessed = true;

            while(isProcessed)
            {
                ShowClientInfo();

                const string RepairCommand = "1";
                const string RefusalCommand = "2";

                Console.WriteLine($"{RepairCommand}. Отремонтировать машину.");
                Console.WriteLine($"{RefusalCommand}. Отказать клиенту.");

                string userInput = Console.ReadLine();

                switch (userInput) 
                {
                    case RepairCommand:
                        Repair(client);
                        break;
                    case RefusalCommand:
                        Refuse(client);
                        break;
                    default:
                        Console.WriteLine("Вы ввели что-то другое. Попробуйте ещё раз.");
                        break;
                }
            }
        }

        private void Repair(Client client)
        {
            _storage.ShowDetails();
        }

        private void Refuse(Client client)
        { 

        }

        private void ShowClientInfo()
        {

        }
    }

    class Client
    {
        private Detail _brokenDetail;

        public Client(string carName, Detail brokenDetail)
        {
            int minRepairPrice = 500;
            int maxRepairPrice = 5000;

            CarName = carName;
            _brokenDetail = brokenDetail;
            _brokenDetail.Break();
            RepairPrice = _brokenDetail.Cost + Utilites.RandomizeNumber(minRepairPrice, maxRepairPrice);
        }

        public string CarName { get; private set; }
        public int RepairPrice { get; private set; }

        public Detail GetDetail() => new Detail(_brokenDetail.Name, _brokenDetail.Cost, _brokenDetail.IsBroken);

    }

    class ClientCreator
    {
        private List<Client> _clients;

        public ClientCreator()
        {
            DetailsCreator detailsTemplate = new DetailsCreator();

            List<Detail> details = detailsTemplate.Details;

            _clients = new List<Client>
            {
                new Client("Toyota Corolla", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Toyota Camry", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Honda CR-V", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Toyota RAV4", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Ford F-Series", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Hyundai Tucson", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Chevrolet Silverado", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Tesla Model 3", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Volkswagen Polo", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
                new Client("Nissan Sylphy", details[Utilites.RandomizeNumber(0, details.Count - 1)]),
            };
        }

        public Client GetRandomizeClient()
        {
            Client randomClient = _clients[Utilites.RandomizeNumber(0, _clients.Count - 1)];
            Client client = new Client(randomClient.CarName, randomClient.GetDetail());
            return client;
        }
    }

    class DetailsStorage
    {
        private List<DetailBox> _detailBoxes;

        public DetailsStorage()
        {
            DetailsCreator detailsCreator = new DetailsCreator();
            List<Detail> details = detailsCreator.Details;
            _detailBoxes = new List<DetailBox>();

            foreach (Detail detail in details)
                _detailBoxes.Add(new DetailBox(detail));

            FillRandomly();
        }

        public List<DetailBox> DetailBoxes => _detailBoxes.ToList();

        public void Add(Detail detail)
        {
            foreach (DetailBox detailCounter in _detailBoxes)
                if (detailCounter.DetailsName == detail.Name)
                    detailCounter.Counter++;
        }

        public void ShowDetails()
        {
            foreach(DetailBox box in _detailBoxes)
                Console.WriteLine($"Название: {box.DetailsName}. Стоимость: {box.DetailsCost}. Количество: {box.Counter}");
        }

        private void FillRandomly()
        {
            int minDetailCount = 0;
            int maxDetailCount = 3;

            foreach (DetailBox box in _detailBoxes)
                box.Counter = Utilites.RandomizeNumber(minDetailCount, maxDetailCount);
        }
    }

    class DetailBox
    {
        private Detail _detail;
        private int _counter;

        public DetailBox(Detail detail)
        {
            _detail = detail;
            _counter = 0;
        }

        public int Counter
        {
            get => _counter;
            set {
                _counter = value;
            }
        }
        public string DetailsName => _detail.Name;
        public int DetailsCost => _detail.Cost;
    }

    class DetailsCreator
    {
        private List<Detail> _details;

        public DetailsCreator()
        {
            _details = new List<Detail>
            {
                new Detail("Двигатель", 50000, false),
                new Detail("Коробка передач", 30000,false),
                new Detail("Сцепление", 25000,false),
                new Detail("Лобовое стекло", 15000,false),
                new Detail("Фары", 10000,false),
                new Detail("Зеркала заднего вида", 5000,false)
            };
        }

        public List<Detail> Details => _details.ToList();
    }

    class Detail
    {
        public Detail(string name, int cost, bool isBroken)
        {
            Name = name;
            Cost = cost;
            IsBroken = isBroken;
        }

        public string Name { get; private set; }
        public int Cost { get; private set; }
        public bool IsBroken { get; private set; }

        public void ShowInfo()
        {

        }

        public void Break() => IsBroken = true;
    }

    class Utilites
    {
        private static Random s_random = new Random();

        public static int RandomizeNumber(int minNumber, int maxMunber)
        {
            return s_random.Next(minNumber, maxMunber + 1);
        }
    }
}