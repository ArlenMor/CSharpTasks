using System;
using System.Collections.Generic;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            Store store = new Store();

            store.Work();
        }
    }

    class Store
    {
        private List<string> _productsName;

        private List<Product> _productsAssortment;
        private Queue<Client> _clients;

        private int _money;

        public Store()
        {
            _productsName = new List<string>
            {
                "Анчоус", "Авокадо", "Арбуз",
                "Баклажан", "Баранина", "Банан",
                "Говядина", "Горбуша", "Гречка",
                "Курица", "Кукуруза", "Кускус",
                "Лук репчатый", "Лимон", "Лапша яичная",
                "Масло подсолнечное", "Масло сливочное", "Мидии",
                "Овсянка", "Окунь", "Огурец",
                "Перец болгарский", "Помидор", "Пиво",
                "Рис", "Ром", "Рак",
                "Телятина", "Треска", "Тыква"
            };

            _clients = new Queue<Client>();
            _productsAssortment = new List<Product>();

            _money = 0;

            CreateProductAssortment();
        }

        public void Work()
        {
            CreateClientQueue();

            while (_clients.Count != 0)
            {
                Console.WriteLine($"Денег у магазина: {_money}");

                Console.WriteLine(">>>Клиентны:");

                int clientCount = 0;

                foreach (Client client in _clients)
                {
                    Console.Write($"{++clientCount}. ");
                    client.ShowInfo();
                }

                Client currentClient = _clients.Dequeue();

                _money += currentClient.Pay();

                Console.WriteLine("Нажмите любую клавишу для продолжения..");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void CreateClientQueue()
        {
            int maxClients = 7;
            int minClients = 4;

            int minClientMoney = 200;
            int maxClientMoney = 5000;

            int maxProductsFromClient = 5;
            int numberOfClients = Utilites.RandomizeNumber(minClients, maxClients);

            for (int i = 0; i < numberOfClients; i++)
            {
                Client client = new Client(Utilites.RandomizeNumber(minClientMoney, maxClientMoney));

                for (int j = 0; j < maxProductsFromClient; j++)
                {
                    int randomIndex = Utilites.RandomizeNumber(0, _productsAssortment.Count - 1);

                    Product product = new Product(_productsAssortment[randomIndex].Name,
                                                    _productsAssortment[randomIndex].Cost);

                    client.TakeProduct(product);
                }

                _clients.Enqueue(client);
            }
        }

        private void CreateProductAssortment()
        {
            int maxProducts = 50;

            int minPrice = 100;
            int maxPrice = 1000;

            for (int i = 0; i < Utilites.RandomizeNumber(1, maxProducts); i++)
                _productsAssortment.Add(new Product(_productsName[Utilites.RandomizeNumber(0, _productsName.Count - 1)],
                                        Utilites.RandomizeNumber(minPrice, maxPrice)));
        }
    }

    class Client
    {
        private List<Product> _products;

        public Client(int money)
        {
            Money = money;
            _products = new List<Product>();
        }

        public int Money { get; private set; }

        public int PriceOfProducts
        {
            get
            {
                int priceOfProducts = 0;

                foreach (Product product in _products)
                    priceOfProducts += product.Cost;

                return priceOfProducts;
            }
        }

        public void ShowInfo()
        {
            Console.Write("Продукты/цена: ");

            foreach (Product product in _products)
                Console.Write($"{product.Name}/{product.Cost}; ");

            Console.WriteLine($"Сумма покупки: {PriceOfProducts}. У клиента {Money}");
        }

        public void TakeProduct(Product product)
        {
            _products.Add(product);
        }

        public int Pay()
        {
            bool isPaid = false;

            while (isPaid == false)
            {
                if (TryBuy())
                {
                    BuyProducts();
                    isPaid = true;
                }
                else
                {
                    Console.WriteLine($"У покупателя {Money} денег, а нужно {PriceOfProducts}. Покупатель убирает из корзины случайный товар...");
                    RemoveRandomProduct();
                }
            }

            return PriceOfProducts;
        }

        private bool TryBuy()
        {
            return Money >= PriceOfProducts;
        }

        private void BuyProducts()
        {
            Console.Write($"У покупателя {Money} денег, а нужно {PriceOfProducts}. ");
            Money -= PriceOfProducts;
            Console.WriteLine($"Покупатель совершает покупку и у него остаётся {Money} денег.");
        }

        private void RemoveRandomProduct()
        {
            if (_products.Count == 0)
                return;

            int remoteIndex = Utilites.RandomizeNumber(0, _products.Count - 1);

            _products.RemoveAt(remoteIndex);
        }
    }

    class Product
    {
        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; private set; }
        public int Cost { get; private set; }
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