using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main()
        {
            const string RubToUsdExchangeCommand = "rub to usd";
            const string RubToEurExchangeCommand = "rub to eur";
            const string UsdToRubExchangeCommand = "usd to rub";
            const string UsdToEurExchangeCommand = "usd to eur";
            const string EurToRubExchangeCommand = "eur to rub";
            const string EurToUsdExchangeCommand = "eur to usd";
            const string ShowWalletExchangeCommand = "show wallet";
            const string CommandExit = "exit";

            string userCommand;

            float rub;
            float usd;
            float eur;
            float purchasePrice;

            float rubToUsd = 0.011f;
            float usdToRub = 90.11f;
            float rubToEur = 0.01f;
            float eurToRub = 99.02f;
            float usdToEur = 0.9f;
            float eurToUsd = 1.11f;

            bool isProgrammRunning = true;

            Console.WriteLine("Добро пожаловать в самый выгодный во всей нашей галактике обменный пункт, странник!!!");
            Console.WriteLine($"Тут только 1 правило: чтобы выйти, напиши {CommandExit}\nИтак, начнём, для начала скажите мне..");
            Console.Write("Сколько у Вас рублей: ");
            rub = Convert.ToSingle(Console.ReadLine());
            Console.Write("Сколько у Вас долларов: ");
            usd = Convert.ToSingle(Console.ReadLine());
            Console.Write("Сколько у Вас евро: ");
            eur = Convert.ToSingle(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Ого, да ты богач.. Навеное.. Что-ж, вот, посмотри на табличку ниже:");

            Console.WriteLine("/---------Курсы-обмена--------\\");
            Console.WriteLine("|---rub---|---usd---|---eur---|");
            Console.WriteLine($"|----1---->--{rubToUsd}--|---------|");
            Console.WriteLine($"|----1---->--------->--{rubToEur}---|");
            Console.WriteLine($"|--{usdToRub}--<----1----|---------|");
            Console.WriteLine($"|---------|----1---->---{usdToEur}---|");
            Console.WriteLine($"|--{eurToRub}--<---------<----1----|");
            Console.WriteLine($"|---------|--{eurToUsd}---<----1----|");
            Console.WriteLine($"\\-----------------------------/\n");

            Console.WriteLine("/---------Ваш-кошелёк----------");
            Console.WriteLine($"|---rub--->---{rub}");
            Console.WriteLine($"|---usd--->---{usd}");
            Console.WriteLine($"|---eur--->---{eur}");
            Console.WriteLine("\\------------------------------");

            Console.WriteLine("Эй, перед тем как Вы начнёте, давайте я Вам кое-что объясню:");
            Console.WriteLine("Чтобы сконвертировать одну валюту в другую, просто напишите название валюты как в таблице, потом добавьте \" to \" и добавьте валюту, в которую вы хотите совершить конвертацию. Всё просто, верно?");
            Console.WriteLine("Вот например: \"usd to rub\" или \"eur to usd\". Я уверен, Вы справитесь");
            Console.WriteLine("Ах да, чтобы посмотреть свой кошелек, введите команду \"show wallet\"");


            while (isProgrammRunning)
            {
                float amountOfPurchased = 0;
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case RubToUsdExchangeCommand:
                        Console.Write("Сколько долларов вы хотите купить? ");
                        amountOfPurchased = Convert.ToSingle(Console.ReadLine());
                        purchasePrice = amountOfPurchased * usdToRub;

                        if (purchasePrice > rub)
                        {
                            Console.WriteLine("Ой, похоже, что у Вас не хватает денег... Попробуйте ещё раз, только уменьшите сумму покупки");
                        }
                        else
                        {
                            rub -= purchasePrice;
                            usd += amountOfPurchased;
                            Console.WriteLine("Сделка совершена.");
                        }
                        break;

                    case RubToEurExchangeCommand:
                        Console.Write("Сколько евро вы хотите купить? ");
                        amountOfPurchased = Convert.ToSingle(Console.ReadLine());
                        purchasePrice = amountOfPurchased * eurToRub;

                        if (purchasePrice > rub)
                        {
                            Console.WriteLine("Ой, похоже, что у Вас не хватает денег... Попробуйте ещё раз, только уменьшите сумму покупки");
                        }
                        else
                        {
                            rub -= amountOfPurchased * eurToRub;
                            eur += amountOfPurchased;
                            Console.WriteLine("Сделка совершена.");
                        }
                        break;

                    case UsdToRubExchangeCommand:
                        Console.Write("Сколько рублей вы хотите купить? ");
                        amountOfPurchased = Convert.ToSingle(Console.ReadLine());
                        purchasePrice = amountOfPurchased * rubToUsd;

                        if (purchasePrice > usd)
                        {
                            Console.WriteLine("Ой, похоже, что у Вас не хватает денег... Попробуйте ещё раз, только уменьшите сумму покупки");
                        }
                        else
                        {
                            usd -= purchasePrice;
                            rub += amountOfPurchased;
                            Console.WriteLine("Сделка совершена.");
                        }
                        break;

                    case UsdToEurExchangeCommand:
                        Console.Write("Сколько евро вы хотите купить? ");
                        amountOfPurchased = Convert.ToSingle(Console.ReadLine());
                        purchasePrice = amountOfPurchased * eurToUsd;

                        if (purchasePrice > usd)
                        {
                            Console.WriteLine("Ой, похоже, что у Вас не хватает денег... Попробуйте ещё раз, только уменьшите сумму покупки");
                        }
                        else
                        {
                            usd -= purchasePrice;
                            eur += amountOfPurchased;
                            Console.WriteLine("Сделка совершена.");
                        }
                        break;

                    case EurToRubExchangeCommand:
                        Console.Write("Сколько рублей вы хотите купить? ");
                        amountOfPurchased = Convert.ToSingle(Console.ReadLine());
                        purchasePrice = amountOfPurchased * rubToEur;

                        if (purchasePrice > eur)
                        {
                            Console.WriteLine("Ой, похоже, что у Вас не хватает денег... Попробуйте ещё раз, только уменьшите сумму покупки");
                        }
                        else
                        {
                            eur -= purchasePrice;
                            rub += amountOfPurchased;
                            Console.WriteLine("Сделка совершена.");
                        }
                        break;

                    case EurToUsdExchangeCommand:
                        Console.Write("Сколько долларов вы хотите купить? ");
                        amountOfPurchased = Convert.ToSingle(Console.ReadLine());
                        purchasePrice = amountOfPurchased * usdToEur;

                        if (purchasePrice > eur)
                        {
                            Console.WriteLine("Ой, похоже, что у Вас не хватает денег... Попробуйте ещё раз, только уменьшите сумму покупки");
                        }
                        else
                        {
                            eur -= purchasePrice;
                            usd += amountOfPurchased;
                            Console.WriteLine("Сделка совершена.");
                        }
                        break;

                    case ShowWalletExchangeCommand:
                        Console.WriteLine("/---------Ваш-кошелёк----------");
                        Console.WriteLine($"|---rub--->---{rub}");
                        Console.WriteLine($"|---usd--->---{usd}");
                        Console.WriteLine($"|---eur--->---{eur}");
                        Console.WriteLine("\\------------------------------");
                        break;

                    case CommandExit:
                        Console.WriteLine("Спасибо, что пользовались нашими услугами. С наступающим 9867 годом!!!");
                        isProgrammRunning = false;
                        break;

                    default:
                        Console.WriteLine("Ого, Вы знаете языки с краёв нашей галактики? Похвально, но давай всё же на общепринятом, я ничего не понял..");
                        break;
                }
            }
        }
    }
}