using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Shop shop = new Shop();

            shop.Work();
        }
    }

    class Shop
    {
        private Seller _seller;
        private Buyer _buyer;

        public Shop()
        {
            Console.Write("Введите количество денег продавца: ");
            int sellersMoney = 0;
            while (sellersMoney == 0)
                sellersMoney = ReadPositivInt(Console.ReadLine());

            Console.Write("Введите количество денег покупателя: ");
            int buyerMoney = 0;
            while (buyerMoney == 0)
                buyerMoney = ReadPositivInt(Console.ReadLine());

            Console.Clear();

            _seller = new Seller(sellersMoney, ProductsInitialization());
            _buyer = new Buyer(buyerMoney);
        }

        public void Work()
        {
            const char ShowByuersProductsCommand = '1';
            const char ShowSellersProductsCommand = '2';
            const char BuyProductCommand = '3';
            const char ExitCommand = '4';

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"Показать инвентарь и кол-во денег покупателя: {ShowByuersProductsCommand}");
                Console.WriteLine($"Показать товары продавца: {ShowSellersProductsCommand}");
                Console.WriteLine($"Купить товары у продавца: {BuyProductCommand}");
                Console.WriteLine($"Закрыть программу: {ExitCommand}");

                char userInput = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (userInput)
                {
                    case ShowByuersProductsCommand:
                        _buyer.ShowInfo();
                        break;

                    case ShowSellersProductsCommand:
                        _seller.ShowInfo();
                        break;

                    case BuyProductCommand:
                        Trade();
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели неверную команду, попробуйте ещё раз...");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void Trade()
        {
            if (_seller.Products.Count == 0)
            {
                Console.WriteLine("У продавца нет товаров...");
                return;
            }

            Console.WriteLine("Данные продавца:");
            _seller.ShowInfo();

            Console.WriteLine($"Количество денег у покупателя: {_buyer.Money}");
            Console.Write("Какой товар купить? Введите номер товара: ");

            int productIndex = 0;

            while (productIndex == 0)
                productIndex = ReadPositivInt(Console.ReadLine());

            Product productForSale = _seller.Products[productIndex - 1];

            if (productForSale.Cost <= _buyer.Money)
            {
                _seller.SellProduct(productForSale);
                _buyer.BuyProduct(productForSale);
            }
            else
            {
                Console.WriteLine("У вас недостаточно денег для приобритения этого товара...");
            }
        }

        private List<Product> ProductsInitialization()
        {
            const ConsoleKey CreateProductCommand = ConsoleKey.Enter;
            const ConsoleKey StopInitializationCommand = ConsoleKey.Escape;

            List<Product> products = new List<Product>();

            bool isCreateProducts = true;

            while (isCreateProducts)
            {
                Console.Write(new string('-', 5));
                Console.Write("Меню создания товара");
                Console.WriteLine(new string('-', 5));

                Console.WriteLine($"Чтобы создать товар и добавить его в магазин, нажмите {CreateProductCommand}.");
                Console.WriteLine($"Чтобы прекратить, нажмите {StopInitializationCommand}.");

                ConsoleKeyInfo userInput = Console.ReadKey();
                Console.WriteLine();

                switch (userInput.Key)
                {
                    case CreateProductCommand:
                        products.Add(CreateProduct());
                        break;

                    case StopInitializationCommand:
                        isCreateProducts = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели неверную команду, попробуйте ещё раз...");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }

            return products;
        }

        private Product CreateProduct()
        {
            string name;
            int cost;

            Console.Write("Введите название товара: ");
            name = Console.ReadLine();

            while(string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Вы ввели пустую строку. Введите название товара:");
                name = Console.ReadLine();
            }

            Console.Write("Введите стоимость товара: ");
            cost = ReadPositivInt(Console.ReadLine());

            while (cost == 0)
            {
                Console.Write("Стоимость товара должна быть положительной. Введите стоимость товара: ");
                cost = ReadPositivInt(Console.ReadLine());
            }

            return new Product(name, cost);
        }

        private int ReadPositivInt(string input)
        {
            int number;

            if (int.TryParse(input, out number) == false)
            {
                Console.WriteLine("Нужно ввести целое положительное число, а не строку.");
                return 0;
            }

            if (number <= 0)
            {
                Console.WriteLine("Нужно ввести целое положительное число.");
                return 0;
            }

            return number;
        }
    }

    public abstract class Person
    {
        public Person()
        {
            Products = new List<Product>();
        }

        public int Money { get; protected set; }
        public List<Product> Products { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Количество денег: {Money}");
            Console.WriteLine("Товары: ");

            for (int i = 0; i < Products.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                Products[i].ShowInfo();
                Console.WriteLine();
            }
        }
    }

    public class Seller : Person
    {
        public Seller(int money, List<Product> products) : base()
        {
            Money = money;
            Products = new List<Product>(products);
        }

        public void SellProduct(Product product)
        {
            Products.Remove(product);

            Money += product.Cost;
        }
    }

    public class Buyer : Person
    {
        public Buyer(int money) : base()
        {
            Money = money;
        }

        public void BuyProduct(Product product)
        {
            Money -= product.Cost;

            Console.WriteLine("Товар успешно приобретен.");

            Products.Add(product);
        }
    }

    public class Product
    {
        public Product(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public int Cost { get; }

        public void ShowInfo()
        {
            Console.Write($"Название: {Name} - Цена: {Cost}");
        }
    }
}