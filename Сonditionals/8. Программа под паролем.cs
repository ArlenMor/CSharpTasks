using System;

namespace IJunior
{
    class Program
    {
        static void Main()
        {
            string password = "password";
            string userInputPassword;
            
            int numberOfTry = 3;

            for (int i = numberOfTry; i > 0; i--)
            {
                Console.WriteLine("Введите пароль: ");
                userInputPassword = Console.ReadLine();
                
                if(userInputPassword == password)
                {
                    Console.WriteLine("В ЯЮНИОР ПОСЛЕДНЯЯ ВОЛНА НА КУРС ПО UNITY!!!");
                    break;
                }
                else
                {
                    if(i != 1)
                    {
                        Console.WriteLine($"Неверный пароль. У вас осталось {i - 1} попытки");
                        continue;
                    }
                    
                    Console.WriteLine("У тебя был шанс. Ты его упустил.\nТЕПЕРЬ ТЫ НИКОГДА НЕ УЗНАЕШЬ СЕКРЕТИК, ЧТО В ЯЮНИОР ПОСЛЕДНЯЯ ВОЛНА НА КУРС ПО UNITY!!!");
                }
            }
        }
    }
}