using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            List<Soldier> soldiers = new List<Soldier>
            {
                new Soldier("Иван","Ак-47","Старший прапорщик", 50),
                new Soldier("Петр","Пистолет Макарова","Младший лейтенант", 100),
                new Soldier("Василий","Пистолет Макарова","Младший лейтенант", 150),
                new Soldier("Георгий","Пистолет Стечкина","Лейтенант", 200),
                new Soldier("Дмитрий","СПП-1","Подполковник", 250),
                new Soldier("Аркадий","Снайперская винтовка Драгунова (СВД)","Генерал-майор", 300),
                new Soldier("Михаил","СВЛК-14С «Сумрак»","Генерал-майор", 300)
            };

            Console.WriteLine("Список всех военослужащих: \n");

            foreach (var soldier in soldiers)
                soldier.ShowInfo();

            var filteredSoldiers = soldiers.Select(soldier => new { Name = soldier.Name, Rank = soldier.Rank });

            Console.WriteLine("\nСписко имя -> звание\n");

            foreach(var soldier in filteredSoldiers)
                Console.WriteLine(soldier.Name + " -> " + soldier.Rank);
        }
    }

    class Soldier
    {
        public Soldier(string name, string weapon, string rank, int termServiceInMonth)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            TermServiceInMonth = termServiceInMonth;
        }

        public string Name { get; private set; }
        public string Weapon { get; private set; }
        public string Rank { get; private set; }
        public int TermServiceInMonth { get; private set; }

        public void ShowInfo() => Console.WriteLine($"{Name}. Оружие: {Weapon}. Звание: {Rank}. Срок службы (в месяцах): {TermServiceInMonth}");
    }
}